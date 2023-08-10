const sendMessageButton = document.getElementById("sendMessageButton");
const messageInput = document.getElementById("messageInput");
const group = document.getElementById("groupDetails");
const groupId = group.getAttribute("data-groupId");
const username = group.getAttribute("data-username");
const divContainers = document.querySelectorAll(".div-message");



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

    connection.on("ReceiveMessage", (message,chatGroupId) => {
        const messageParts = message.split(",");
        const senderName = messageParts[0];
        const content = messageParts[1];
        const sendAt = messageParts[2];

        if (groupId === chatGroupId) {
            const messageElement = document.createElement("div");
            messageElement.classList.add("message");

            if (senderName === username) {
                messageElement.classList.add("sender");
            }
            else {
                messageElement.classList.add("receiver");
            }

            const usernameElement = document.createElement("strong");
            usernameElement.classList.add("chat-username");
            usernameElement.textContent = senderName;

            const timestampElement = document.createElement("p");
            timestampElement.classList.add("hidden");
            timestampElement.textContent = sendAt;

            const contentContainer = document.createElement("div");
            contentContainer.classList.add(
                "message-content-" + (senderName === username ? "sender" : "receiver")
            );

            const contentDiv = document.createElement("div");
            contentDiv.classList.add("content");
            contentDiv.textContent = content;

            contentContainer.appendChild(contentDiv);
            messageElement.appendChild(timestampElement);
            messageElement.appendChild(usernameElement);
            messageElement.appendChild(contentContainer);

            contentContainer.addEventListener("click", () => {
                timestampElement.classList.toggle("hidden");
            });
            document.getElementById("chatBox").appendChild(messageElement);
            scrollToBottom();
        }
    });

divContainers.forEach((container) => {
    container.addEventListener("click", () => {
        const timestampElement = container.querySelector("p")
        timestampElement.classList.toggle("hidden");
    });
})

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

        scrollToBottom();
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
