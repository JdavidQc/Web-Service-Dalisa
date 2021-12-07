$(document).ready(function () {
 
    $("#body").append(`<!-- Modal -->
<div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">¿Preparado para partir?</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        Seleccione "Cerrar sesión" a continuación si está listo para finalizar su sesión actual.
      </div>
      <div class="modal-footer">
<form action="/Usuario/CerrarSeccion" method="GET">   <button  type="submit" class="btn btn-primary" data-bs-dismiss="modal" >Cerrar </button>
</from>
      </div>
    </div>
  </div>
</div>
`);

    abrirAndCerrar_Menu();
    subirInicioHTML();
    $("#cerrarSeccion").click(function () {

        location.href = "https://localhost:64395/Usuario/CerrarSeccion"
    })
});

function abrirAndCerrar_Menu() {
    let arrow = document.querySelectorAll(".arrow");
    let sidebar = document.querySelector(".sidebar");
    let sidebarBtn = document.querySelector(".bx-menu");
    let sidebarBtn1 = document.querySelector(".bx-x");
    for (var i = 0; i < arrow.length; i++) {
        arrow[i].addEventListener("click", (e) => {
            let arrowParent = e.target.parentElement.parentElement;//selecting main parent of arrow
            arrowParent.classList.toggle("showMenu");
        });
    }
    sidebarBtn.addEventListener("click", () => {
        sidebar.classList.toggle("close");
    });
    sidebarBtn1.addEventListener("click", () => {
        sidebar.classList.toggle("close");
    });
}

function subirInicioHTML() {
    $('.subir').click(function () {
        $('body, html').animate({
            scrollTop: '0px'
        }, 300);
    });

    $(window).scroll(function () {
        if ($(this).scrollTop() > 50) {
            $('.subir').slideDown(300);
        } else {
            $('.subir').slideUp(300);
        }
    });
}

