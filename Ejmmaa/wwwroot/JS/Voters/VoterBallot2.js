let selectedCandidates = [];
let maxSelectionsAllowed = 0;

// تنفيذ الكود تلقائياً بمجرد تحميل الصفحة
$(document).ready(function () {
    loadCandidates();

    getMaxSelections(function (maxSelections) {
        maxSelectionsAllowed = maxSelections;
        $('#maxLimit').text(maxSelections);
        $('#maxAllowed').text(maxSelections);
    });
});

// 1. جلب بيانات المرشحين من السيرفر باستخدام jQuery AJAX
function loadCandidates() {


    apiRetrieve(

        '/Candidates/GetAllCandidates', // ضع هنا المسار الصحيح للـ API لديك
        null, // لا توجد بيانات إضافية للإرسال في هذه الحالة
        function (response) {
            if (response.success) {
                renderCandidates(response.data);
            }
        },
        function (xhr, status, error) {
            console.error("خطأ أثناء جلب بيانات المرشحين:", error);
            $('#membersList').html(`
              <div class="col-12 text-center text-danger py-4">
                  <i class="bi bi-exclamation-triangle fs-1"></i>
                  <p class="mt-2">عذراً، تعذر تحميل بيانات المرشحين. يرجى تحديث الصفحة.</p>
              </div>
          `);
        }

    );


}

// 2. توليد وعرض بطاقات المرشحين داخل الـ HTML
function renderCandidates(candidates) {
    const container = $('#membersList');
    container.empty(); // تفريغ الحاوية قبل الإضافة

    if (!candidates || candidates.length === 0) {
        container.html('<div class="col-12 text-center text-muted">لا توجد بيانات مرشحين متاحة حالياً.</div>');
        return;
    }

    candidates.forEach(candidate => {
        // يمكنك تعديل خصائص الكائن (id, fullName, imagePath) لتتطابق مع الـ Model القادم من السيرفر
        const cardHtml = `
            <div class="col-md-3 col-sm-6">
                <div class="card candidate-card text-center p-3 h-100 rounded-3" 
                     onclick="toggleCandidate(this, ${candidate.candidateId}, '${candidate.fullName}')" 
                     data-id="${candidate.candidateId}">
                    <div class="custom-check"><i class="bi bi-check-lg d-none"></i></div>
                    <div class="bg-light rounded-circle mx-auto d-flex align-items-center justify-content-center mb-3" style="width: 80px; height: 80px;">
                        ${candidate.imagePath ? `<img src="${candidate.imagePath}" class="rounded-circle w-100 h-100 object-fit-cover" alt="${candidate.fullName}">` : '<i class="bi bi-person text-secondary fs-2"></i>'}
                    </div>
                    <h5 class="fw-bold mb-1 fs-6">${candidate.fullName}</h5>
                    <span class="text-muted fs-7">رقم المرشح: ${candidate.candidateId}</span>
                </div>
            </div>
        `;
        container.append(cardHtml);
    });
}

function getMaxSelections(callback) {
    apiRetrieveCustomValue(
        '/Elections/GetMaxSelection',
        null,
        function (resp) {
            // التصحيح: استخدام resp للوصول للخصائص القادمة من الـ JSON
            if (resp.success === true) {
                // إرجاع القيمة عبر الـ callback
                if (typeof callback === 'function') {
                    callback(resp.data);
                }
            } else {
                showToast(resp.message, 'error');
                if (typeof callback === 'function') callback(0);
            }
        },
        function (error) {
            // التصحيح: استخدام error أو رسالة عامة لأن resp غير معرف هنا
            var errorMsg = error && error.message ? error.message : "حدث خطأ أثناء الاتصال";
            showToast(errorMsg, 'error');
            if (typeof callback === 'function') callback(0);
        }
    );
}

// 3. دالة تحديد / إلغاء تحديد المرشح
function toggleCandidate(element, candidateId, candidateName) {
    const $card = $(element);
    const $checkIcon = $card.find('.custom-check i');
    const index = selectedCandidates.findIndex(c => c.id === candidateId);

    if (index > -1) {
        // إلغاء التحديد
        selectedCandidates.splice(index, 1);
        $card.removeClass('selected');
        $checkIcon.addClass('d-none');
    } else {
        // التحقق من الحد الأقصى المسموح به
        if (selectedCandidates.length >= maxSelectionsAllowed) {
            showToast(`عذراً، يمكنك اختيار حد أقصى هو ${maxSelectionsAllowed} مرشحين فقط.`, 'warning');
            return;
        }
        // إضافة التحديد
        selectedCandidates.push({ id: candidateId, name: candidateName });
        $card.addClass('selected');
        $checkIcon.removeClass('d-none');
    }

    $('#selectedCount').text(selectedCandidates.length);
}

// 4. الانتقال لصفحة الملخص
function goToSummary() {
    if (selectedCandidates.length === 0) {
        showToast('الرجاء اختيار مرشح واحد على الأقل للمتابعة.', 'warning');
        return;
    }

    $('#stepBallot').addClass('d-none');
    $('#stepSummary').removeClass('d-none');

    const $summaryList = $('#selectedMembersSummary');
    $summaryList.empty();

    selectedCandidates.forEach(candidate => {
        $summaryList.append(`
            <li class="mb-2 d-flex align-items-center">
                <i class="bi bi-check-circle-fill text-success ms-2"></i> 
                <span>${candidate.name} (رقم: ${candidate.id})</span>
            </li>
        `);
    });
}

// 5. العودة لورقة الاقتراع للتعديل
function backToBallot() {
    $('#stepSummary').addClass('d-none');
    $('#stepBallot').removeClass('d-none');
}

// 6. إرسال التصويت النهائي للسيرفر
function submitFinalVote() {
    if (confirm('هل أنت متأكد من رغبتك في اعتماد وإرسال صوتك نهائياً؟ لا يمكن التراجع بعد الإرسال.')) {
        let candidateIds = selectedCandidates.map(c => c.id);

        // إرسال البيانات عبر AJAX (باستخدام apiService أو jQuery مباشرة)

        apiAdd(
            '/Voters/SubmitVote',
            candidateIds,
            function (resp) {
                if (success == true) {
                    showToast(resp.message, 'success');
                    window.location.href = '/Voters/Success'; // التوجيه لصفحة النجاح
                    e
                }

            },
            function (error) {
                showToast('حدث خطأ أثناء إرسال الصوت. يرجى المحاولة مرة أخرى.', 'error');
                console.error(xhr.responseText);

            }
        );
        /*
        $.ajax({
            url: '/Voters/SubmitVote', // مسار استقبال الأصوات في الـ Controller
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ candidateIds: candidateIds }),
            success: function (response) {
                showToast('تم تسجيل وصوتك بنجاح وسرية تامّة!', 'success');
                window.location.href = '/Voters/Success'; // التوجيه لصفحة النجاح
            },
            error: function (xhr) {
                showToast('حدث خطأ أثناء إرسال الصوت. يرجى المحاولة مرة أخرى.', 'error');
                console.error(xhr.responseText);
            }
        });*/
    }
}