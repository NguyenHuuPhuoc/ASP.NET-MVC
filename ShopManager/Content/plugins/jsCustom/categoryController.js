var config = {
    id: null
}

var Category = {
    init: function () {
        Category.events();
    },
    events: function () {
        $('#TypeId').off('change').on('change', function () {
            $('#frmFilter').submit();
        });

        $('#ParentId').off('change').on('change', function () {
            $('#frmFilter').submit();
        });

        $('#btnCreateNew').off('click').on('click', function () {
            var typeId = $('#TypeId').val();
            var parentId = $('#ParentId').val();
            location.href = "/Category/CreateNew?parentId=" + parentId + "&typeId=" + typeId;
        });

        $('.editUnit').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            location.href = "/Category/Update?id=" + id;
        });

        $('.deleteUnit').off('click').on('click', function (e) {
            config.id = $(this).data('id');
            $('#modalConfirmDelete').show();
        });

        $('#btnOkDelete').off('click').on('click', function (e) {
            $('#modalConfirmDelete').hide();
            var id = config.id;
            config.id = null;
            $('#frm_' + id).submit();
        });

        $('.viewDetail').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            $.ajax({
                url: "/Category/ViewDetail",
                data: { id: id },
                type: "post",
                dataType: "json",
                success: function (result) {
                    var model = result.data;

                    $('.lblType').text(model.Type);
                    $('.lblParent').text(model.Parent);
                    $('.lblName').text(model.Name);
                    $('.lblCode').text(model.Code);
                    $('.lblCreateBy').text(model.CreateBy);
                    $('.lblCreateDate').text(model.CreateDate);
                    $('.lblUpdateBy').text(model.UpdateBy);
                    $('.lblUpdateDate').text(model.UpdateDate);
                    $('.lblDescreption').text(model.Descreption);
                    $('.lblStatus').text(model.Status);

                    $('#modalViewDetail').modal();
                }
            });
        });
    }
}

Category.init();