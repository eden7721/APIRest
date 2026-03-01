export async function recuperarDatos(periodoValue, tiempoValue, tipoValue, estadoValue) {
    let route = "https://localhost:7125/Ventas/";
    console.log(`${route}${periodoValue}${tipoValue}/${tiempoValue}/${estadoValue}`);

    if (periodoValue === "global") {
        if (estadoValue === "ambos") {
            let response = [await fetch(`${route}${periodoValue}${tipoValue}/true`), await fetch(`${route}${periodoValue}${tipoValue}/false`)];
            response.forEach(async el => {
                let json = await el.json();
                console.log(json);
            })
        }
        else {
            let response = await fetch(`${route}${periodoValue}${tipoValue}/${estadoValue}`);
            let json = await response.json();
            console.log(json);
        }
    }
    else if (estadoValue === "ambos") {
        let response = [await fetch(`${route}${periodoValue}${tipoValue}/${tiempoValue}/true`), await fetch(`${route}${periodoValue}${tipoValue}/${tiempoValue}/false`)];
        response.forEach(async el => {
            let json = await el.json();
            console.log(json);
        });

    } else {
        let response = await fetch(`${route}${periodoValue}${tipoValue}/${tiempoValue}/${estadoValue}`);
        let json = await response.json();
        console.log(json);
    }








}
export async function recuperarDatosPorValor(periodoValue, tiempoValue) {

}