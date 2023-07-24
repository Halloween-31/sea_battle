try {
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

import { duel } from "./ts/duel_fight/duel_fight.js"

try {
    const hubConnection = new signalR.HubConnectionBuilder()
        .withUrl("/chat")
        .build();

    hubConnection.start()
        .then(function () {
            console.log("Connect is ok!");

            /*const conId = hubConnection.connectionId;
            const uesrID = hubConnection.UserIdentifier;
            fetch("/user/SetCurrentConId", {
                method: "PUT",
                headers: { "Accept": "application/json", "Content-Type": "application/json" },
                body: JSON.stringify(uesrID)
            });*/
        })
        .catch(function (err) {
            return console.error(err.toString());
        });    

    hubConnection.on("Invitation", function (enemyName, email, myEmail) {
        let answer = confirm(`Вас викликає на дуель ` + enemyName);
        if (answer == false) {
            hubConnection.invoke("Answer", email, false);
        }
        else {
            hubConnection.invoke("Answer", email, true);
            window.location.href = "/Game_duel/GameField?enemyEmail=" + encodeURIComponent(email) +
                "&myEmail=" + encodeURIComponent(myEmail) + 
                "&sender=false";
        }
    });

    hubConnection.on("AnswerAfterInvitation", (ans, myEmail) => {
        if (ans == false) {
            alert("Гравець відмовився!");
        }
        else {
            alert("Гравець погодився!");
            window.location.href = "/Game_duel/GameField?enemyEmail=" + encodeURIComponent(getCookie("EnemyEmail")) +
                "&myEmail=" + encodeURIComponent(myEmail) +
                "&sender=true";
        }
    });

    duel(hubConnection);
}
catch (e) {
    console.error("Connection error: " + e);
};

function getCookie(name) {
    let matches = document.cookie.match(new RegExp(
        "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
    ));
    return matches ? decodeURIComponent(matches[1]) : undefined;
}


hubConnection.on("FullField", async (EnemyField) => {
    alert("homepagelink!");
});