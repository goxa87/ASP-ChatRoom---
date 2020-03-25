$(document).ready(function () {
    // Запрос на обращение к АПИ пролучение списка сообщений.
    function ajaxRequestGetMessages(theLogin, thePassword, common, adress) {

        $.ajax(
            {
                dataType: 'json',
                url: adress,
                data: {
                        login: theLogin,
                        password: thePassword,
                        opponentId: common
                },
                success: function (response) {
                    console.log(response);
                    ChangeBody(response);
                },
                fail: function () { console.log("ОШИБКА ЗАГРУЗКИ"); } 
            }
        );
    }
    // Добавление Полученных сообщений в список(замена при изменении пользователя).
    function ChangeBody(response)
    {
        var rezult = "<div class=\"chat-message-list\">";

        if (response == null) {
        }
        else {
            $(response).each(function (index, value) {
                rezult += '<div class="message"><span class="message-title">' + value.date
                    + ' - ' + value.senderName + '</span ><p class="message-body">' + value.text + '</p ></div>';
            });
        }
        rezult += "</div>";
        $('.chat-message-list').replaceWith(rezult);
    }

    // Нажатие на пользователя в левом списке.
    $('.opponent').click(function () {
        var opponentId = $(this).children('.opponent-id').text();
        $('#opponent').text(opponentId);
        // Изсенение выделенного оппонента . Применение стилей.
        $('.selected-opponent').toggleClass('selected-opponent').toggleClass('unselected-opponent');
        $(this).toggleClass('selected-opponent');
        $(this).toggleClass('unselected-opponent');

        // Получение данных для запроса.
        var name = $(this).children('.opponent-pseudonym').text();
        console.log($('#opponent-name').text());
        $('#opponent-name').text(name);
        var login = $('#login').text();
        var password = $('#password').text();
        ajaxRequestGetMessages(login, password, opponentId, '/api/MessageApi/GetMessages');
        
    })

    // Запрос на отправку сообщения.
    function SendMessage(theLogin, thePassword, opponent, messageText, adres)
    {
        $.ajax(
            {
                url: adres,
                data:
                {
                    login: theLogin,
                    password: thePassword,
                    opponentId: opponent,
                    text: messageText
                },
                success: function () { console.log("отправлено: " + messageText); }
            }
        );
    }

    // Событие нажатия кнопки отправки.
    $('.btn-send-message').click(function ()
    {
        var text = $('#text-value').val();

        if (text == "")
            alert("Пустое сообщение");
        else {
            $('#text-value').val('');

            var login = $('#login').text();
            var password = $('#password').text();            
            var opponent = $('#opponent').text();
            console.log(opponent);
            SendMessage(login, password, opponent, text, '/api/MessageApi/SendMessage');
            // Добавляем на страницу.
            var block = '<div class="message"><span class="message-title">' + Date.now()
                + ' - ' + login + '</span ><p class="message-body">' + text + '</p ></div>';

            $('.chat-message-list').prepend(block);
        }
    });   
});

  