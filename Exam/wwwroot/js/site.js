$('#SendBtn').click(function (e) {
    var articleId = $('#articleId').val();
   
    var data = { "id": articleId};
    $.ajax({
        url: '/Exam/Check/',
        type: 'post',
        data: data,
       dataType: 'JSON',
      success: function (dataList) {

            for (let i = 0; i < dataList.result.length; ++i) {
                console.log(dataList.result[i]["Key"]);
                var el="#"+dataList.result[i]["Key"];
              $(el).parent().css("background-color", "red");

            }
          return;
        },

    });



});