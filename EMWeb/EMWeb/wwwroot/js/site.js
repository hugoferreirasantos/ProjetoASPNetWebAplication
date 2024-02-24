document.addEventListener('DOMContentLoaded', function () {

    var deleteButtons = document.querySelectorAll('.delete-button');
    deleteButtons.forEach(function (button) {
        button.addEventListener('click', function (e) {
            e.preventDefault();

            var alunoId = this.getAttribute('data-aluno-id');

            var deleteButton = document.getElementById('deleteButton');
            deleteButton.setAttribute('href', '/Ficha/Excluir?id=' + alunoId);

            var confirmDeleteModalElement = document.getElementById('confirmDeleteModal');
            var confirmDeleteModal = new bootstrap.Modal(confirmDeleteModalElement);
            confirmDeleteModal.show();
        });
    });

    document.getElementById("myButton").addEventListener("click", function () {
        this.innerHTML = '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Carregando...';
    });

    window.onload = function () {

        var errorMessage = document.getElementById('alertMessageErro').textContent;
        if (errorMessage) {
            $('#customAlertErroJavascrip').modal('show');
        }



    }


});