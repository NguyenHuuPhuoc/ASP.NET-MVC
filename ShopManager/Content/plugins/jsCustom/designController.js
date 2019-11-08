var config = {
    id: null
}

var Design = {
    init: function () {
        Design.events();
    },
    events: function () {
        $('#ParentCategoryId').off('change').on('change', function () {
            $('#frmFilter').submit();
        });

        $('#btnCreateNew').off('click').on('click', function () {
            var parentCategoryId = $('#ParentCategoryId').val();
            location.href = "/Design/CreateNew?parentCategoryId=" + parentCategoryId;
        });

        $('.editUnit').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            location.href = "/Design/Update?id=" + id;
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
                url: "/Design/ViewDetail",
                data: { id: id },
                type: "post",
                dataType: "json",
                success: function (result) {
                    var model = result.data;

                    $('.lblName').text(model.Name);
                    $('.lblCreateBy').text(model.CreateBy);
                    $('.lblCreateDate').text(model.CreateDate);
                    $('.lblUpdateBy').text(model.UpdateBy);
                    $('.lblUpdateDate').text(model.UpdateDate);
                    $('.lblDescreption').text(model.Descreption);
                    $('.lblStatus').text(model.Status);
                    $('.lblParentCategory').text(model.ParentCategory);

                    $('#modalViewDetail').modal();
                }
            });
        });
    }
}
Design.init();