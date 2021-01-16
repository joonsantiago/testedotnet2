export class FunctionsUtil {

    
    showElemId(elem, show) {
        var e = document.getElementById(elem);
        if (show) {
            e.classList.remove('hidden');
        } else {
            e.classList.add('hidden');
        }
    }

    habledElemId(elem, show) {
        var e = document.getElementById(elem);
        if (show) {
            e.classList.remove('disabled');
        } else {
            e.classList.add('disabled');
        }
    }



 }