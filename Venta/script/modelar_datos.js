import { datos } from "./recuperar_datos.js";

const d = document,
    $form = d.getElementById("form-1"),
    $btnCargarDatos = d.getElementById("btn-cargar-datos"),
    $btnEnviarDatos = d.getElementById("btn"),
    $selectorPeriodo = d.getElementById("selector-periodo"),
    $selectorTiempo = d.getElementById("selector-tiempo"),
    $selectorTipo = d.getElementById("selector-tipo"),
    $productos = d.getElementById("productos-obtenidos");

d.addEventListener("click",e => {
    if(e.target.matches("#btn-enviar-datos")){
        e.preventDefault();
        datos($selectorPeriodo.value, $selectorTipo.value, $selectorTiempo.value);
    }
});
d.addEventListener("change", e => {
    if(e.target.matches("#selector-periodo")) {
        $selectorTiempo.innerHTML = null;
        let $template = d.getElementById(`template-${$selectorPeriodo.value}`).content;
        let clone = d.importNode($template, true);
        $selectorTiempo.appendChild(clone);
    }
});

