// chat.js
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chathub")
    .build();

connection.start()
    .then(() => {
        console.log("Connected to SignalR hub.");
    })
    .catch((error) => {
        console.error("Error connecting to SignalR hub:", error);
    });

connection.on("ReceiveMessage", (message) => {
    const messageElement = document.createElement("div");
    messageElement.textContent = message;
    document.getElementById("chatBox").appendChild(messageElement);
});

const sendMessageButton = document.getElementById("sendMessageButton");
const messageInput = document.getElementById("messageInput");
const group = document.getElementById("groupDetails");
const groupId = group.getAttribute("data-groupId");

sendMessageButton.addEventListener("click", () => {
    const message = messageInput.value;
    if (message.trim() !== "") {
        const model = {
            Content: message,
            SendAt: new Date()
        };

        connection.invoke("SendMessageToGroup", model, groupId)
            .then(() => {
                messageInput.value = "";
            })
            .catch((error) => {
                console.error("Error sending message:", error);
            });
    }
});

function updateSendButtonState() {
    var messageInput = document.getElementById("messageInput");
    var sendButton = document.getElementById("sendMessageButton");
    sendButton.disabled = messageInput.value.trim() === "";
}

function scrollToBottom() {
    var chatBox = document.getElementById("chatBox");
    chatBox.scrollTop = chatBox.scrollHeight;
}

document.getElementById("messageInput").addEventListener("keypress", function (event) {
    if (event.key === "Enter") {
        sendMessage();
    }
});

document.getElementById("messageInput").addEventListener("input", function () {
    updateSendButtonState();
});

window.onload = function () {
    scrollToBottom();
};
