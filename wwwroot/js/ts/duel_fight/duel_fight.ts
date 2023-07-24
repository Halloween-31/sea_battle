export function duel(connection) {
    const email: HTMLInputElement = document.getElementById('email') as HTMLInputElement;    

    async function CheckEmail() {
        try {
            const res = await fetch("/user/IsEmailExists", {
                method: "POST",
                headers: { "Accept": "application/json", "Content-Type": "application/json" },
                body: JSON.stringify(email.value)
            })
            const IsExist = await res.json();
            if (IsExist == false) {
                alert("Даного користувача не існує");
                return;
            }
            else {
                document.cookie = encodeURIComponent("EnemyEmail") + "=" + encodeURIComponent(email.value) + "; path=/;";

                /*const myEmailContainer: NodeListOf<HTMLDataListElement> = document.querySelectorAll('dt') as NodeListOf<HTMLDataListElement>;
                let myEmailValue;
                myEmailContainer.forEach(e => {
                    if (e.nodeValue.includes("@")) {
                        myEmailValue = e.nodeValue;                        
                    }
                });*/

                /*const url = window.location.href;
                const myId = parseInt(url.split('/').pop());

                const res = await fetch("/user/GetMyEmail", {
                    method: "POST",
                    headers: { "Accept": "application/json", "Content-Type": "application/json" },
                    body: JSON.stringify(myId)
                })
                const myEmail = await res.json();*/

                connection.invoke("SendInvitation", email.value)
                    .then(res => {
                        console.log(res);
                    })
                    .catch(err => {
                        console.error("Unable to connect with another gamer");
                        return console.error(err.toString());
                    });
            }
        }
        catch (e) {
            console.error("Fetch error: " + e);
        }
    }


    try {
        const btn: HTMLButtonElement = document.getElementById("find") as HTMLButtonElement;
        btn.addEventListener("click", CheckEmail);
    }
    catch (e) {
        console.log("No invitation\n" + e);
    }
}
class User {
    name: string;
    surname: string;
}