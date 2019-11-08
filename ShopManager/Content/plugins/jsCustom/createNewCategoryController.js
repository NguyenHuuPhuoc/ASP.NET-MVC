var C = {
    init: function () {
        C.events();
    },
    events: function () {
        $('#Type').off('change').on('change', function () {
            var typeId = $(this).val();

            $.ajax({
                url: "/Category/SetParentsByTypeId",
                data: { typeId: typeId },
                type: "post",
                dataType: "json",
                success: function (result) {
                    var lst = result.data;
                    var htmlStr = "<select id='ParentId'>";
                    htmlStr += "<option value=''>--Là danh mục cha--</option>";
                    for (var i = 0; i < lst.length; i++){
                        htmlStr += "<option value="+ lst[i].Id +">"+ lst[i].Name +"</option>";
                    }
                    htmlStr += "</select>";
                    $('#ParentId').html(htmlStr);
                }
            });
        });

        $('#btnCancel').off('click').on('click', function () {
            location.href = "/Category";
        });

        $('#Code').off('blur').on('blur', function () {
            var code = $(this).val();

            if(code == '' || code == null){
                return false;
            } else {
                $.ajax({
                    url: "/Category/SetCode",
                    data: { code: code },
                    type: "post",
                    dataType: "json",
                    success: function (result) {
                        var newCode = result.data;
                        $('#Code').val(newCode);
                    }
                });
            }
        });
    }
}
C.init();