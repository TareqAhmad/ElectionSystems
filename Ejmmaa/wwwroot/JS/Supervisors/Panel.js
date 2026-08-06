/* ==========================================================================
   Supervisor Panel JS - Ejmaa System
   ========================================================================== */

$(document).ready(function () {

    // 1. حدث الضغط على زر البحث
    $("#btnSearchVoter").on("click", function () {
        searchVoter();
    });

    // البحث عند الضغط على زر Enter داخل حقل الإدخال
    $("#searchVoterInput").on("keypress", function (e) {
        if (e.which === 13) {
            searchVoter();
        }
    });

    // 2. حدث إرسال الـ OTP
    $("#btnGenerateOTP").on("click", function () {
        sendOTP();
    });
});

// دالة البحث عن المقترع عبر الـ API
function searchVoter() {
    const query = $("#searchVoterInput").val().trim();

    if (query === "") {
        Swal.fire({
            icon: 'warning',
            title: 'تنبيه',
            text: 'يرجى إدخال الرقم الوطني أو رقم الهاتف أولاً.',
            confirmButtonText: 'موافق'
        });
        return;
    }

    // إظهار مؤشر التحميل الخاص بـ SweetAlert2 أثناء جلب البيانات
    Swal.showLoading();

    // استدعاء الـ API الخاص ببيانات الناخبين (تغيير المسار حسب الـ Controller لديك)
    $.ajax({
        url: '/Supervisors/GetVoterDetails', 
        type: 'GET',
        data: { searchKey: query },
        success: function (response) {
            Swal.close(); // إغلاق مؤشر التحميل

            if (response.success) {
                // تعبئة البيانات المسترجعة داخل كرت النتيجة
                $("#lblVoterName").text(response.data.fullName);
                $("#lblVoterNationalId").text(response.data.nationalId);
                $("#lblVoterPhone").text(response.data.phoneNumber);
                
                // تحديث حالة التصويت وتغيير لون الكرت بناءً عليها
                if (response.data.hasVoted) {
                    $("#lblVoteStatus").text("قام بالتصويت سابقاً")
                                      .removeClass("bg-warning text-dark")
                                      .addClass("bg-danger text-white");
                    $("#btnGenerateOTP").prop("disabled", true)
                                      .text("تم التصويت - لا يمكن إرسال رمز");
                } else {
                    $("#lblVoteStatus").text("لم يصوت بعد")
                                      .removeClass("bg-danger text-white")
                                      .addClass("bg-warning text-dark");
                    $("#btnGenerateOTP").prop("disabled", false)
                                      .html('<i class="bi bi-shield-fill-check me-1"></i> توليد وإرسال رمز التحقق (OTP)');
                }

                // إظهار صندوق النتائج بتأثير حركة سلسة
                $("#voterResultBox").removeClass("d-none").hide().fadeIn(400);

            } else {
                // إخفاء صندوق النتائج في حال لم يتم العثور على اسم
                $("#voterResultBox").addClass("d-none");
                
                Swal.fire({
                    icon: 'error',
                    title: 'لم يتم العثور على الناخب',
                    text: response.message || 'الرقم الوطني غير مدرج في كشوفات هذه الدائرة الانتخابية.',
                    confirmButtonText: 'موافق'
                });
            }
        },
        error: function () {
            Swal.close();
            Swal.fire({
                icon: 'error',
                title: 'خطأ في النظام',
                text: 'حدث خطأ أثناء الاتصال بالخادم، يرجى المحاولة لاحقاً.',
                confirmButtonText: 'موافق'
            });
        }
    });
}

// دالة توليد وإرسال الـ OTP للناخب المختار
function sendOTP() {
    const nationalId = $("#lblVoterNationalId").text();

    Swal.fire({
        title: 'هل تريد إرسال الـ OTP؟',
        text: "سيتم إرسال رمز تحقق مؤقت ومؤمن إلى هاتف الناخب المعتمد.",
        icon: 'question',
        showCancelButton: true,
        confirmButtonColor: '#22c55e',
        cancelButtonColor: '#64748b',
        confirmButtonText: 'نعم، أرسل الرمز',
        cancelButtonText: 'إلغاء'
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.showLoading();

            $.ajax({
                url: '/Supervisors/GenerateAndSendOTP',
                type: 'POST',
                data: { nationalId: nationalId },
                success: function (response) {
                    Swal.close();
                    if (response.success) {
                        Swal.fire({
                            icon: 'success',
                            title: 'تم إرسال الرمز!',
                            text: 'تم توليد الرمز وإرساله بنجاح بنظام الـ SMS.',
                            confirmButtonText: 'رائع'
                        });
                        
                        // إضافة العملية إلى جدول "آخر عمليات التحقق" كنشاط فوري
                        const newActivity = `
                            <div class="verified-item d-flex justify-content-between align-items-center p-3 rounded-3 mb-2 border" style="display:none;">
                                <span class="text-muted small">الآن</span>
                                <div class="text-end">
                                    <h6 class="fw-bold mb-0 small">${$("#lblVoterName").text()}</h6>
                                    <span class="badge bg-success-light text-success fs-9">تم توليد الرمز</span>
                                </div>
                            </div>`;
                        
                        $(".verified-list").prepend(newActivity);
                        $(".verified-list .verified-item:first").slideDown(300);

                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: 'فشل الإرسال',
                            text: response.message || 'حدثت مشكلة أثناء إرسال رسالة الـ SMS.',
                            confirmButtonText: 'موافق'
                        });
                    }
                },
                error: function () {
                    Swal.close();
                    Swal.fire({
                        icon: 'error',
                        title: 'خطأ',
                        text: 'فشل الاتصال بالخادم لإرسال الـ OTP.',
                        confirmButtonText: 'موافق'
                    });
                }
            });
        }
    });
}