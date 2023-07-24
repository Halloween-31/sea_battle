/*const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/chat")
    .build();

document.querySelector(".btn").addEventListener("click", function () {
    const msg = document.querySelector('.form-container > textarea').value;

    hubConnection.invoke("Send", msg)
        .catch(function (err) {
            return console.error(err.toString());
        });
});

hubConnection.on("Receive", function (message) {
    console.log("Recieveed message: " + message);
});

hubConnection.start()
    .then(function () {
        console.log("Connect is ok!");
    })
    .catch(function (err) {
        return console.error(err.toString());
    });*/