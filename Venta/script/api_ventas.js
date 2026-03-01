import { recuperarDatos } from "./recuperar_datos.js";

const d = document,
    $form = d.getElementById("form-1"),
    $btnCargarDatos = d.getElementById("btn-cargar-datos"),
    $btnEnviarDatos = d.getElementById("btn"),
    $selectorPeriodo = d.getElementById("selector-periodo"),
    $selectorTiempo = d.getElementById("selector-tiempo"),
    $selectorTipo = d.getElementById("selector-tipo"),
    $selectorEstado = d.getElementById("selector-estado");

d.addEventListener("click", e => {
    if(e.target.matches("#btn-enviar-datos")){
        e.preventDefault();
        recuperarDatos($selectorPeriodo.value, $selectorTiempo.value, $selectorTipo.value, $selectorEstado.value);
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

