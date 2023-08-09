function sendMessage() {
    var messageInput = document.getElementById("messageInput");
    var groupInput = document.getElementById("groupId");
    var groupId = groupInput.value.trim();
    var message = messageInput.value.trim();
    if (message === "") {
        return;
    }

    var sendMessageData = {
        groupId: groupId,
        content: message
    };

    $.post(sendMessageUrl, sendMessageData)
        .done(function (response) {
            messageInput.value = "";
            document.getElementById("sendMessageButton").disabled = true;
            location.reload();
        })
        .fail(function (error) {
            console.error("Failed to send message:", error);
        });
}

function updateSendButtonState() {
    var messageInput = document.getElementById("messageInput");
    var sendButton = document.getElementById("sendMessageButton");
    sendButton.disabled = messageInput.value.trim() === "";
}

function scrollToBottom() {
    var chatBox = document.getElementById("chatBox");
    chatBox.scrollTop = chatBox.scrollHeight;
}


function leaveGroup() {
    var groupInput = document.getElementById("groupId");
    var groupId = groupInput.value.trim();

    $.post(leaveGroupUrl, { groupId})
}
document.getElementById("sendMessageButton").addEventListener("click", function () {
    sendMessage();
});

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

document.getElementById("leaveGroupButton").addEventListener("click", function () {
    leaveGroup();
});
