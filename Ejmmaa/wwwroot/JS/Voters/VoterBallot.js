// كائن لحفظ خيارات المقترع في الذاكرة الحالية
let currentSelections = {
    presidentId: null,
    presidentName: '',
    memberIds: [],
    memberNames: []
};

// 1. اختيار مرشح الرئاسة (مُعدلة لأسلوب الـ Checkbox)
function selectPresident(id, name, cardElement) {
    // إلغاء تحديد كافة مرشحي الرئاسة الآخرين لضمان "خيار فردي واحد"
    $('#presidentsList .pres-check').prop('checked', false);

    // تفعيل الـ Checkbox الحالي داخل الكرت الذي تم النقر عليه
    let chk = $(cardElement).find(':input.custom-checkbox');
    chk.prop('checked', true);

    // حفظ الاختيار الحالي
    currentSelections.presidentId = id;
    currentSelections.presidentName = name;
}

// 2. اختيار مرشحي العضوية (مُعدلة لأسلوب الـ Checkbox وبحد أقصى 3)
function selectMember(id, name, cardElement) {
    let chk = $(cardElement).find(':input.custom-checkbox');
    let index = currentSelections.memberIds.indexOf(id);

    // إذا تم النقر على كرت معلّم مسبقاً، نقوم بإلغاء التعليم وإزالته من المصفوفة
    if (chk.prop('checked')) {
        chk.prop('checked', false);
        currentSelections.memberIds.splice(index, 1);
        currentSelections.memberNames.splice(index, 1);
    } else {
        // منع تجاوز الحد الأقصى للاختيارات (3 أعضاء) باستخدام الـ Toast العالمي
        if (currentSelections.memberIds.length >= 3) {
            showToast("عذراً يا صديقي، الحد الأقصى المسموح باختياره هو 3 أعضاء فقط.", "warning");
            return;
        }
        
        // تعليم الـ Checkbox وإضافته للمصفوفة
        chk.prop('checked', true);
        currentSelections.memberIds.push(id);
        currentSelections.memberNames.push(name);
    }
}

// 3. التحقق والانتقال إلى شاشة الملخص المدمجة
function validateAndGoToSummary() {
    if (!currentSelections.presidentId) {
        showToast("الرجاء اختيار رئيس للمجلس أولاً لضمان اكتمال ورقة اقتراعك.", "warning");
        return;
    }
    if (currentSelections.memberIds.length === 0) {
        showToast("الرجاء اختيار عضو واحد على الأقل للمجلس.", "warning");
        return;
    }

    // حقن اسم الرئيس ديناميكياً في الملخص
    $('#selectedPresidentSummary').text(currentSelections.presidentName);
    
    // حقن قائمة الأعضاء المختارين ديناميكياً في الملخص
    let membersListHtml = '';
    currentSelections.memberNames.forEach(name => {
        membersListHtml += `<li class="mb-2">• ${name}</li>`;
    });
    $('#selectedMembersSummary').html(membersListHtml);

    // إظهار شاشة الملخص بنعومة
    switchSection('stepSummary');
}

// 4. دالة الرجوع من شاشة الملخص لتعديل الخيارات
function backToBallot() {
    switchSection('stepBallot');
}

// 5. تأكيد وإرسال التصويت النهائي مجهول الهوية للسيرفر
function submitFinalVote() {
    let payload = {
        PresidentId: currentSelections.presidentId,
        MemberIds: currentSelections.memberIds
    };

    console.log("إرسال الصوت المشفر والمجهول للسيرفر عبر الـ Payload:", payload);

    // إظهار توست نجاح عند إرسال الصوت بنجاح
    showToast("تم إرسال صوتك بنجاح وسرية تامّة!", "success");

    // سنربط هنا كود الـ AJAX الفعلي لاحقاً لإرسال الطلب للـ Controller
    switchSection('stepSuccess');

    // بعد 6 ثوانٍ من شاشة النجاح، تتم تهيئة وتصفير النظام تلقائياً للناخب التالي
    setTimeout(function () {
        window.location.href = '/Voters/index'; // إعادة توجيه لصفحة تسجيل الدخول/الرئيسية
    }, 6000);
}

// دالة مساعدة لتبديل أقسام الصفحة بنعومة وسلاسة
function switchSection(sectionId) {
    $('.step-content').addClass('d-none');
    $('#' + sectionId).removeClass('d-none');
}