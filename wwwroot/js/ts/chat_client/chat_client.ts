async function isLoggined() {
    try {
        const isLoggined = await fetch("/Game/isLoggined", {
            method: "GET",
            headers: { "Accept": "application/json", "Content-Type": "application/json" }
        });
        const _isLoggined = await isLoggined.json();
        return _isLoggined;
    }
    catch (e) {
        console.log("Fetch throw error!");
        console.error(e);
    }
}

function openForm() {
    document.getElementById("myForm").style.display = "block";
    this.parentElement.style.display = 'none';
}

function closeForm() {
    document.getElementById("myForm").style.display = "none";
    const btn_chat = document.querySelector('.open-button');
    btn_chat.parentElement.style.display = 'block';
}

async function Checking() {
    try {
        const _isLoggined = await isLoggined();
        if (_isLoggined == "true") {
            const btn_chat = document.querySelector('.open-button');
            btn_chat.addEventListener('click', openForm);            
            document.querySelector('.cancel').addEventListener('click', closeForm);           
        }
        else {
            const btn_chat = document.querySelector('.open-button');
            btn_chat.parentElement.style.display = 'none';
        }
    } catch (e) {
        console.error(e);
    }    
}

Checking();