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

myBtns[0].removeEventListener('click', clickOnMyBtn);
myBtns[0].addEventListener("click", async () => {
    const _fetch = await fetch("/Game_duel/MyFieldQuick", {
        method: "GET",
        headers: {
            'Accept': 'application/json; charset=utf-8',
            'Content-Type': 'application/json;charset=UTF-8'
        }
    });
    const res: string = await _fetch.json();

    const arrShips: string[] = res.split(/[\ '\n']/).filter((word) => {
        if (word != "") {
            return word;
        }
    });

    arrShips.forEach(item => {
        const num: number = parseInt(item);
        myBtns[num].style.background = 'darkslateblue';
    });

    if (!enabledDone) {
        EnableAll();
    }
});
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

hubConnection.serverTimeoutInMilliseconds = 600000;

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
        return;
    }
    //

    const pos: number = this.positionXY;  
    let obj = {
        x: Math.trunc((pos / 10)),
        y: pos - (Math.trunc((pos / 10)) * 10),
        answer: []
    };

    const urlParams = new URLSearchParams(window.location.search);
    const sender = urlParams.get('sender');
    const enemyEmail = urlParams.get('enemyEmail');

    let response = await fetch("/Game_duel/Attacking?" +
        encodeURIComponent("enemyEmail") + "=" + encodeURIComponent(enemyEmail) + "&" +
        encodeURIComponent("sender") + '=' + encodeURIComponent(sender), {
        method: "POST",
        headers: {
            'Accept': 'application/json; charset=utf-8',
            'Content-Type': 'application/json;charset=UTF-8'
        },
        body: JSON.stringify(obj)
    });
    // 0 - не влучив, 1 - влучив, -1 - знищив; для ворога аналогічно  
    let message = await response.json();

    if (message === false) {
        alert("Хід противника!");
        return;
    }   

    (AfterAttackUI.bind(this))(message, 0);
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
    else {
        alert("Error");
    }
});

function backToenable() {
    //  робимо можливим натискання на кнопки знову!
    setTimeout(() => {
        for (let i = myBtns.length / 2; i < myBtns.length; i++) {
            myBtns[i].disabled = false;
        }
    }, 500);
    //
    return true;
}

function disableAll() {
    for (let i = myBtns.length / 2; i < myBtns.length; i++) {
        myBtns[i].disabled = true;
    }
}

async function AfterAttackUI(message: number, fieldIndex: number) {
    const firstInterval: number = 500;
    const secondInterval: number = 2000;

    switch (message) {
        case 0: {            // не влучив
            disableAll();
            this.style.backgroundColor = 'black';

            let timer = setInterval(() => {
                if (this.style.backgroundColor == 'black') {
                    this.style.backgroundColor = 'white';
                } else if (this.style.backgroundColor == 'white') {
                    this.style.backgroundColor = 'black';
                }
            }, firstInterval)

            setTimeout(() => {
                this.style.backgroundColor = 'black';
                clearInterval(timer);

                setTimeout(() => {
                    backToenable();
                }, firstInterval);

            }, secondInterval);
        }
            break;
        case 1: { // влучив
            disableAll();
            this.style.backgroundColor = 'red';

            let timer = setInterval(() => {
                if (this.style.backgroundColor == 'black') {
                    this.style.backgroundColor = 'red';
                } else if (this.style.backgroundColor == 'red') {
                    this.style.backgroundColor = 'black';
                }
            }, firstInterval)

            setTimeout(() => {
                this.style.backgroundColor = 'red';
                clearInterval(timer);

                setTimeout(() => {
                    backToenable();
                }, firstInterval);

            }, secondInterval);
        }
            break;
        case -1: {      // знищив
            this.style.backgroundColor = 'red';
            const timer = setTimeout(async () => {
                let deadShipAnswer = await fetch(`/Game_duel/deadEnemyShip/${this.positionXY}`);
                let deadShipArr = await deadShipAnswer.json();
                if (deadShipArr == -1) {
                    console.log("Не знайшло знищений корабель!");
                    return;
                }
                deadShip(this, (fieldIndex == 0 ? 1 : 0), deadShipArr); // 0 - я, 1 - ворог
                setTimeout(() => {
                    backToenable();
                }, secondInterval);
            }, firstInterval);           

            // перевірка чи не кінець
            let isEnd = await fetch(`/Game_duel/isEnd/${fieldIndex}`);  // 0 - я чи виграв, 1 - ворог чи виграв
            let isEndRes = await isEnd.json();
            if (isEndRes == true) {
                if (fieldIndex == 0) {
                    let divWithH = document.querySelector("main > div > div > div");

                    let h3ToInsert = document.createElement('h3');
                    h3ToInsert.textContent = "Ви виграли!";

                    divWithH.append(h3ToInsert);

                    alert("Ви виграли!");

                    // унеможливлюємо нажаття на кнопки                    
                    for (let i = myBtns.length / 2; i < myBtns.length; i++) {
                        myBtns[i].disabled = true;
                    }                    
                }
                else if (fieldIndex == 1) {
                    let divWithH = document.querySelector("main > div > div > div");

                    let h3ToInsert = document.createElement('h3');
                    h3ToInsert.textContent = "Ви програли!";

                    divWithH.append(h3ToInsert);

                    alert("Ви програли!");

                    // унеможливлюємо нажаття на кнопки
                    for (let i = myBtns.length / 2; i < myBtns.length; i++) {
                        myBtns[i].disabled = true;
                    }


                    let enemyFieldLication = await fetch("/Game/enemyField");
                    let resOfEnemyLocation = await enemyFieldLication.json();

                    for (let i = 0; i < resOfEnemyLocation.length; i++) {
                        for (let j = 0; j < resOfEnemyLocation[i].length; j++) {
                            if (myBtns[resOfEnemyLocation[i][j] + 100].style.backgroundColor != 'red') {
                                myBtns[resOfEnemyLocation[i][j] + 100].style.backgroundColor = 'green';
                            }
                        }
                    }
                }
            }            
        }
            break;
        default:
            console.error("Server failed! Return number after checking hiting is corrupt!");
            break;
    }
}

interface XY {
    _x: number,
    _y: number,
    hited: boolean
}

hubConnection.on("Attack", async (cell: XY, isHit: number) => {
    const pos: number = cell._x * 10 + cell._y;
    // сихронізувати тут хто ходить!
    if (isHit == 0) {
        const _res = await fetch("/Game_duel/SynchrStep", {
            method: "PUT",
            headers: {
                'Accept': 'application/json; charset=utf-8',
                'Content-Type': 'application/json;charset=UTF-8'
            }
        });
        const res = await _res.json();
        if (res == true) {
            console.log("Synchronize step");
        }
        else {
            alert("Error!");            
        }
    }
    else {
        const _res = await fetch("/Game_duel/SynchrHit", {
            method: "PUT",
            headers: {
                'Accept': 'application/json; charset=utf-8',
                'Content-Type': 'application/json;charset=UTF-8'
            },
            body: JSON.stringify(cell)
        });
        const res = await _res.json();
        if (res == true) {
            console.log("Synchronize hit");
        }
        else {
            alert("Error synchronization!");
        }
    }

    (AfterAttackUI.bind(myBtns[pos]))(isHit, 1);
});

function deadShip(lastBtn: HTMLButtonElement, fieldIndex: number, someArr) {
    let pos = (lastBtn as any).positionXY;

    let arrValue = [
        [-11, -10, -9],
        [-1, 0, 1],
        [9, 10, 11]
    ];

    let nextPos = [-1, -1, -1, -1, -1];
    nextPos[0] = pos;


    let someBtn = [];
    if (fieldIndex == 0) {
        for (let i = 0; i < myBtns.length / 2; i++) {
            someBtn.push(myBtns[i]);
        }

        for (let position of someArr) {
            if (position[0] != 11) {
                let somePos = position[0] * 10 + position[1];

                try {
                    for (let i = 0; i < 3; i++) {
                        for (let j = 0; j < 3; j++) {

                            if ((somePos % 10) == 0) {
                                if (j == 0) {
                                    continue;
                                }
                            }

                            if ((somePos % 10) == 9) {
                                if (j == 2) {
                                    continue;
                                }
                            }

                            if (somePos < 10) {
                                if (i == 0) {
                                    continue;
                                }
                            }

                            if (somePos > 90) {
                                if (i == 2) {
                                    continue;
                                }
                            }

                            if ((somePos + arrValue[i][j] < 0) || (somePos + arrValue[i][j] > 99)) {
                                continue;
                            }

                            if (someBtn[somePos + arrValue[i][j]].style.backgroundColor != 'red') {
                                someBtn[somePos + arrValue[i][j]].style.backgroundColor = 'black';
                            }
                        }
                    }
                }
                catch (err) {
                    console.log(err);
                    console.log(err.stack);
                    console.log(someBtn);
                }
            }
        }
    } else if (fieldIndex == 1) {
        for (let i = myBtns.length / 2; i < myBtns.length; i++) {
            someBtn.push(myBtns[i]);
        }
        for (let position of someArr) {
            if (position[0] != 11) {
                let somePos = position[0] * 10 + position[1];

                try {
                    for (let i = 0; i < 3; i++) {
                        for (let j = 0; j < 3; j++) {

                            if ((somePos % 10) == 0) {
                                if (j == 0) {
                                    continue;
                                }
                            }

                            if ((somePos % 10) == 9) {
                                if (j == 2) {
                                    continue;
                                }
                            }

                            if (somePos < 10) {
                                if (i == 0) {
                                    continue;
                                }
                            }

                            if (somePos > 90) {
                                if (i == 2) {
                                    continue;
                                }
                            }

                            if ((somePos + arrValue[i][j] < 0) || (somePos + arrValue[i][j] > 99)) {
                                continue;
                            }

                            if (someBtn[somePos + arrValue[i][j]].style.backgroundColor != 'red') {
                                someBtn[somePos + arrValue[i][j]].style.backgroundColor = 'black';
                            }
                        }
                    }
                }
                catch (err) {
                    console.log(err);
                    console.log(err.stack);
                    console.log(someBtn);
                }
            }
        }
    }
}