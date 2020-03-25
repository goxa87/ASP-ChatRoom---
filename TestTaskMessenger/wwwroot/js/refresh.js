// обновление сообщений раз в 5 сек.
setInterval(function () { getData() }, 5000);

// Формирование данных и вызов запроса.
function getData()
{
    console.log('+ refresh');

    var login = $('#login').text();
    var password = $('#password').text();
    var opponent = $('#opponent').text();

    if (opponent == "0") return;

    RefreshCurentList(login, password, opponent);
}
// Запрос на сервер.
function RefreshCurentList(login, password, opponentId) {
    $.ajax(
        {
            url: '/api/MessageApi/GetCurrentUnreadMessages',
            dataType: 'json',
            data: {
                login: login,
                password: password,
                opponentId: opponentId
            },
            success: function (response) {
                UpdateList(response);
            }
        });
}
// Парсинг полученого результата в разметку.
function UpdateList(response) {
    // Если ничего не пришло выходим.
    if (response == null)
        return;

    var block = '';
    $(response).each(function (index, value) {
        block += '<div class="message"><span class="message-title">' + value.date
            + ' - ' + value.senderName + '</span ><p class="message-body">' + value.text + '</p ></div>';
        $('.chat-message-list').prepend(block);
    });
}    