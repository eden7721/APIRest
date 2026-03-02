const d = document,
    $template = d.getElementById("template-registro").content,
    $fragment = d.createDocumentFragment(),
    $registros = d.getElementById("cargar-registros");

async function recuperarRegistros() {
    try {
        const host = window.location.hostname === "localhost" ? "https://localhost:7125" : "https://192.168.142.162:7125";
        console.log(host);
        let url = `${host}/Ventas`;
        let response = await fetch(url);
        let json = await response.json();

        json.forEach(el => {
            let clon = d.importNode($template, true);
            clon.querySelector(".card-producto").textContent = el.producto;
            clon.querySelector(".card-categoria").textContent = el.categoria;
            clon.querySelector(".card-cliente").textContent = el.cliente;
            clon.querySelector(".card-precio").textContent = el.precio_unitario;
            clon.querySelector(".card-cantidad").textContent = el.cantidad;
            clon.querySelector(".card-valor").textContent = el.total_venta;
            clon.querySelector(".card-fecha").textContent = el.fecha_compra;
            $fragment.appendChild(clon);
        });
        $registros.appendChild($fragment);
        
    }
    catch(err) {
        
        console.log(err)
    }
}
d.addEventListener("click", e => {
    if(e.target.matches("#btn-recuperar-ventas")){
        $registros.innerHTML = null;
        recuperarRegistros();
    }
});

