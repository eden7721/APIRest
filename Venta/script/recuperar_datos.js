async function recuperarDatos(periodoValue, tipoValue, tiempoValue) {
    const route = "https://localhost:7125/Ventas/";
    console.log(`${route}${periodoValue}/${tipoValue}/${tiempoValue}`);

    const urlProductoMejores = periodoValue === "global"
        ? `${route}${periodoValue}/${tipoValue}/true`
        : `${route}${periodoValue}/${tipoValue}/${tiempoValue}/true`;

    const urlProductoPeores = periodoValue === "global"
        ? `${route}${periodoValue}/${tipoValue}/false`
        : `${route}${periodoValue}/${tipoValue}/${tiempoValue}/false`;

    const urlCategoriaMejores = `${route}categoria/${periodoValue}/${tipoValue}/${tiempoValue}/true`;
    const urlCategoriaPeores = `${route}categoria/${periodoValue}/${tipoValue}/${tiempoValue}/false`;

    const [responseProductoMejores, responseProductoPeores, responseCategoriaMejores, responseCategoriaPeores] = await Promise.all([
        fetch(urlProductoMejores),
        fetch(urlProductoPeores),
        fetch(urlCategoriaMejores),
        fetch(urlCategoriaPeores)
    ]);

    const [jsonProductoMejores, jsonProductoPeores, jsonCategoriaMejores, jsonCategoriaPeores] = await Promise.all([
        responseProductoMejores.json(),
        responseProductoPeores.json(),
        responseCategoriaMejores.json(),
        responseCategoriaPeores.json()
    ]);

    return [jsonProductoMejores, jsonProductoPeores, jsonCategoriaMejores, jsonCategoriaPeores];
}


export async function datos(periodoValue, tipoValue, tiempoValue) {

    const datos = await recuperarDatos(periodoValue, tipoValue, tiempoValue),
        d = document,
        $productos = d.querySelector(".productos"),
        $mejorProducto = d.querySelector(".mejor-producto"),
        $peorProducto = d.querySelector(".peor-producto"),
        $graficas = d.querySelector(".graficas"),
        $graficaMejoresP = d.querySelector(".grafica-mejores-productos"),
        $graficasPeoresP = d.querySelector(".grafica-peores-productos"),
        $graficaCategorias = d.querySelector(".grafica-categorias"),
        $graficaCategoriasMP = d.querySelector(".grafica-categoria-mejores-productos"),
        $graficaCategoriasPP = d.querySelector(".graficas-categoria-peores-productos"),
        $fragment = d.createDocumentFragment();

    d.querySelectorAll(".title-tipo").forEach(el => {
        el.textContent = (tipoValue === "true") ? "Cantidad" : "Valor";
    })
    if(periodoValue === "mes"){
        let periodoTitle = "mensual";
        d.querySelector(".title-periodo").textContent = periodoTitle;
    }
    else if(periodoValue === "trimestre") {
        let periodoTitle = "mensual";
        d.querySelector(".title-periodo").textContent = periodoTitle;
    }
    else {
        let periodoTitle = "anual";
        d.querySelector(".title-periodo").textContent = periodoTitle;
    }
    //Mejor producto
    $mejorProducto.querySelector(".producto").textContent = `${datos[0][0].producto}`;
    $mejorProducto.querySelector(".categoria").textContent = `${datos[0][0].categoria}`;
    $mejorProducto.querySelector(".cantidad-venta").textContent = `${datos[0][0].cantidadTotalVendida}`;
    $mejorProducto.querySelector(".valor-venta").textContent = `S/${datos[0][0].valorTotalVenta}`;


    //Peor producto
    $peorProducto.querySelector(".producto").textContent = `${datos[1][0].producto}`;
    $peorProducto.querySelector(".categoria").textContent = `${datos[1][0].categoria}`;
    $peorProducto.querySelector(".cantidad-venta").textContent = `${datos[1][0].cantidadTotalVendida}`;
    $peorProducto.querySelector(".valor-venta").textContent = `S/${datos[1][0].valorTotalVenta}`;


    //Gráficos

    function generarColores(num) {
        const colores = [];
        for (let i = 0; i < num; i++) {
            const r = Math.floor(Math.random() * 156) + 100;
            const g = Math.floor(Math.random() * 156) + 100;
            const b = Math.floor(Math.random() * 156) + 100;
            colores.push(`rgba(${r}, ${g}, ${b}, 0.7)`);
        }
        return colores;
    }


    function crearGrafico(datos, tipoValue, backgroundColor, borderColor, canvasId) {
        const productos = [];
        const valores = [];
        let tipo = "";

        for (const item of datos) {
            productos.push(item.producto);
            if (tipoValue === "true") {
                valores.push(item.cantidadTotalVendida);
                tipo = "Unidades vendidas";
            } else {
                valores.push(item.valorTotalVenta);
                tipo = "Valor de venta";
            }
        }

        const canvas = document.getElementById(canvasId);

        // Destruir gráfico previo si existe
        if (Chart.getChart(canvas)) {
            Chart.getChart(canvas).destroy();
        }

        return new Chart(canvas.getContext('2d'), {
            type: 'bar',
            data: {
                labels: productos,
                datasets: [{
                    label: tipo,
                    data: valores,
                    backgroundColor: backgroundColor,
                    borderColor: borderColor,
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                plugins: {
                    legend: { position: 'top' },
                    title: { display: true, text: 'Ventas por producto' }
                },
                scales: { y: { beginAtZero: true } }
            }
        });
    }

    function crearGraficoPie(datos, tipoValue, canvasId) {
        const productos = [];
        const valores = [];
        let tipo = "";

        for (const item of datos) {
            productos.push(item.nombreCategoria);
            if (tipoValue === "true") {
                valores.push(item.cantidadTotalVendida);
                tipo = "Unidades vendidas";
            } else {
                valores.push(item.valorVentaTotal);
                tipo = "Valor de venta";
            }
        }

        const canvas = document.getElementById(canvasId);

        // Destruir gráfico previo si existe
        if (Chart.getChart(canvas)) {
            Chart.getChart(canvas).destroy();
        }

        const colores = generarColores(productos.length);

        return new Chart(canvas.getContext('2d'), {
            type: 'pie',
            data: {
                labels: productos,
                datasets: [{
                    label: tipo,
                    data: valores,
                    backgroundColor: colores,
                    borderColor: colores.map(c => c.replace('0.7', '1')),
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                plugins: {
                    legend: { position: 'top' },
                    title: { display: true, text: 'Ventas por producto' }
                }
            }
        });
    }

    // ----------------------------- CREACIÓN DE GRÁFICOS -----------------------------

    let miGrafico1 = crearGrafico(datos[0], tipoValue, 'rgba(54, 162, 235, 0.7)', 'rgba(54, 162, 235, 1)', "miGrafico1");
    let miGrafico2 = crearGrafico(datos[1], tipoValue, 'rgba(235, 54, 87, 0.7)', 'rgb(235, 54, 63)', "miGrafico2");
    let miGrafico3 = crearGraficoPie(datos[2], tipoValue, "miGrafico3");
    let miGrafico4 = crearGraficoPie(datos[3], tipoValue, "miGrafico4");

}
