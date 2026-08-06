let selectedCandidateId = null;

// إدارة تفعيل الأقسام وتغير الكور العليا (Steps Animation)
function goToStep(stepNum) {
    // إخفاء كل المحتويات
    document.querySelectorAll('.step-content').forEach(el => el.classList.add('d-none'));
    
    // إظهار المحتوى المطلوب
    document.getElementById('step' + stepNum).classList.remove('d-none');
    
    // تحديث مؤشر الكور (Indicators)
    for (let i = 1; i <= 4; i++) {
        let node = document.getElementById('node' + i);
        if (i < stepNum) {
            node.className = 'step-node completed';
            node.innerHTML = '<i class="fa-solid fa-check"></i>';
        } else if (i === stepNum) {
            node.className = 'step-node active';
            node.innerHTML = i;
        } else {
            node.className = 'step-node';
            node.innerHTML = i;
        }
    }
}

// دالة اختيار مرشح وتنشيط الكرت
function selectCandidate(id, cardElement) {
    // إزالة التنشيط عن بقية المرشحين
    document.querySelectorAll('.candidate-card').forEach(card => card.classList.remove('selected'));
    
    // تنشيط الكرت الحالي
    cardElement.classList.add('selected');
    selectedCandidateId = id;
}

// إعادة تشغيل النظام وتصفير الحقول بعد خروج الناخب
function resetVoterJourney() {
    selectedCandidateId = null;
    document.getElementById('voterNationalId').value = '';
    document.getElementById('otpCode').value = '';
    document.querySelectorAll('.candidate-card').forEach(card => card.classList.remove('selected'));
    goToStep(1);
}
