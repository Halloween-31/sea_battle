try {
    /*let URL;
    if (window.location.href.includes('user')) {
        URL = 'GetCurrentUserId';
        const userID = await fetch(URL, {
            method: "GET"
        });

        const id = await userID.json();
        console.log(id)

        if (id != -1 && id != null) {
            const a_home = document.getElementById('Home');

            a_home.setAttribute("href", "HomePage?id=" + id);
        }
    }
    else {
        URL = 'user/GetCurrentUserId';
        const userID = await fetch(URL, {
            method: "GET"
        });

        const id = await userID.json();
        console.log(id)

        if (id != -1 && id != null) {
            const a_home = document.getElementById('Home');

            a_home.setAttribute("href", "user/HomePage?id=" + id);
        }
    }*/
    let URL = '/user/GetCurrentUserId'
    const userID = await fetch(URL, {
        method: "GET"
    });

    const id = await userID.json();
    console.log(id)

    if (id != -1 && id != null) {
        const a_home = document.getElementById('Home');

        a_home.setAttribute("href", "/user/HomePage?id=" + id);
    }
}
catch (e) {
    console.error(e);
}