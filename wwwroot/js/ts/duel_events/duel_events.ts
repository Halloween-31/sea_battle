const myBtns : NodeListOf<HTMLButtonElement> = document.querySelectorAll("table > tbody > tr > td > button");
for (let i = 0; i < myBtns.length / 2; i++) {
    (myBtns[i] as any).positionXY = i;
    myBtns[i].addEventListener("click", clickOnMyBtn);
}

for (let i = myBtns.length / 2; i < myBtns.length; i++) {
    (myBtns[i] as any).positionXY = i - (myBtns.length / 2);
    myBtns[i].disabled = true;
    myBtns[i].addEventListener("click", clickAtack);
}

let enabledDone: boolean = false;
async function clickOnMyBtn() {
    const pos: number = this.positionXY;

    if (this.style.backgroundColor == 'darkslateblue') {
        alert("Ця клітинка вже вибрана!");
        return;
    }

    const XY_obj = {        
        x: Math.trunc((pos / 10)),
        y: pos - (Math.trunc((pos / 10)) * 10),
        answer: null
    };

    const urlParams = new URLSearchParams(window.location.search);
    const sender = urlParams.get('sender');    

    const responce = await fetch("/Game_duel/clickMyBtn?"
        + encodeURIComponent("sender") + "=" + encodeURIComponent(sender), {
        method: "POST",
        headers: {
            'Accept': 'application/json; charset=utf-8',
            'Content-Type': 'application/json;charset=UTF-8'
        },
        body: JSON.stringify(XY_obj)
    });
    const answer = await responce.json();

    if (answer == 1) {
        this.style.backgroundColor = 'darkslateblue';
    }
    else if (answer == 0) {
        alert("Неможна вибрати дану клітинку!");
    }
    else {
        alert("Bи викоритсали всі кораблі!");        
    }
    if (!enabledDone) {
        EnableAll();
    }
}

import * as signalR from "../../../lib/microsoft/signalr/dist/browser/signalr.js";
// в .js файлі коментуємо дану стрічку і вона буде брати дане значення з бібліотеки, яка
// записана в .html файлі
// дикий костиль

const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/chat")
    .build();

hubConnection.start()
    .then(function () {
        console.log("Connect is ok!(duel_events)");
    })
    .catch(function (err) {
        return console.error(err.toString());
    });    

let getEnemyField: boolean = false;
async function clickAtack() {    
    if (this.style.backgroundColor == 'red' || this.style.backgroundColor == 'black') {
        alert("Ця клітинка вже вибрана! Точніше знищена!");
        return;
    }

    //перевірка чи заповнив все супротивник
    if (!getEnemyField) {
        alert("Очікуємо противника!");
    }
    //

    const pos: number = this.positionXY;
}

async function EnableAll() {
    let counter: number = 0;
    for (let i = 0; i < myBtns.length / 2; i++) {
        if (myBtns[i].style.backgroundColor == 'darkslateblue') {
            counter++;
        }
    }

    if (counter == 20) {
        for (let i = myBtns.length / 2; i < myBtns.length; i++) {
            myBtns[i].disabled = false;        
        }

        enabledDone = true;

        const urlParams = new URLSearchParams(window.location.search);
        const EnemyEmail = urlParams.get('enemyEmail');  

        await fetch("/Game_duel/FullField", {
            method: "PUT",
            headers: { "Accept": "application/json", "Content-Type": "application/json" },
            body: JSON.stringify(EnemyEmail)
        });
    }
}

hubConnection.on("FullField", async (EnemyField: string) => {
    const res = await fetch("/Game_duel/EnemyField", {
        method: "PUT",
        headers: {
            'Accept': 'application/json; charset=utf-8',
            'Content-Type': 'application/json;charset=UTF-8'
        },
        body: JSON.stringify(EnemyField)
    });
    const answer = await res.json();
    if (answer == true) {
        getEnemyField = true;
    }
});