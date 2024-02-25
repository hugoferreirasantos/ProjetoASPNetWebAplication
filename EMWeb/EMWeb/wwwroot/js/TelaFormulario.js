window.onload = function () {
    var successMessage = document.getElementById('alertMessage').textContent;
    if (successMessage) {
        $('#customAlert').modal('show');
    }

    var errorMessage = document.getElementById('alertMessageErro').textContent;
    if (errorMessage) {
        $('#customAlertErroJavascrip').modal('show');
    }



}






var input = document.getElementById('NASCIMENTO');
input.addEventListener('input', function (e) {
    this.type = 'text';
    if (this.value.length == 2 || this.value.length == 5) {
        this.value += '/';
    }
});

input.addEventListener('blur', function (e) {
    this.type = 'text';
    var match = this.value.match(/^(\d{2})\/(\d{2})\/(\d{4})$/);
    if (match) {
        var year = parseInt(match[3], 10);
        var month = parseInt(match[2], 10) - 1;
        var day = parseInt(match[1], 10);
        var date = new Date(year, month, day);
        if (date.getFullYear() !== year || date.getMonth() != month || date.getDate() !== day) {
            this.value = '';
        }
    } else {
    }
});

input.addEventListener('keydown', function (e) {

    if (e.key === 'Delete') {

        this.value = '';
    }
});

input.addEventListener('blur', function (e) {
    this.type = 'text';
    var match = this.value.match(/^(\d{2})\/(\d{2})\/(\d{4})$/);
    if (match) {
        var year = parseInt(match[3], 10);
        var month = parseInt(match[2], 10) - 1; // months are 0-11
        var day = parseInt(match[1], 10);
        var date = new Date(year, month, day);
        if (date.getFullYear() !== year || date.getMonth() != month || date.getDate() !== day) {
            alert("Data Inválida");
            this.value = '';
        }
    } else {
        alert("Formato inválido, digite a data no formato DD/MM/AAAA. ");
        this.value = '';
    }
});


document.addEventListener('DOMContentLoaded', function () {
    var NASCIMENTO = document.getElementById('NASCIMENTO');
    NASCIMENTO.addEventListener('keydown', function (e) {
        if (e.which == 8) {
            var inputVal = this.value;
            var newCursorPos = this.selectionStart;
            if (inputVal[newCursorPos - 1] == "/") {
                this.value = inputVal.substring(0, newCursorPos - 1) + inputVal.substring(newCursorPos);
                this.selectionStart = newCursorPos - 1;
                this.selectionEnd = newCursorPos - 1;
                e.preventDefault();
            }
        }
    });
});


document.getElementById('cpf').addEventListener('input', function (e) {
    var value = this.value.replace(/\D/g, "");
    value = value.replace(/(\d{3})(\d)/, "$1.$2");
    value = value.replace(/(\d{3})(\d)/, "$1.$2");
    value = value.replace(/(\d{3})(\d{1,2})$/, "$1-$2");
    this.value = value;
});

function formatarCPF(cpf) {
    var value = cpf.replace(/\D/g, "");
    value = value.replace(/(\d{3})(\d)/, "$1.$2");
    value = value.replace(/(\d{3})(\d)/, "$1.$2");
    value = value.replace(/(\d{3})(\d{1,2})$/, "$1-$2");
    return value;
}

document.getElementById('cpf').value = formatarCPF(document.getElementById('cpf').value);
