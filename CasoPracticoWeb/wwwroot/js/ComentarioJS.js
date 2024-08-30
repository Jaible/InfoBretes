const stars = document.querySelectorAll('.star');

stars.forEach(function (star, RegistrarComentario) {
    star.addEventListener('click', function () {
        for (let i = 0; i <= RegistrarComentario; i++) {
            stars[i].classList.add('checked');
        }
        for (let i = RegistrarComentario + 1; i < stars.length; i++) {
            stars[i].classList.remove('checked');
        }
    })
})