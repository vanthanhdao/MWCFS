//var product = {
//    init: function () {
//        product.registerEvents();
//    },
//    registerEvents: function () {

//        $('#btn-img').off('click').on('click', function (e) {

//            e.preventDefault();
//            $('#imagesManange').modal('show');
//            $('#hidProductID').val($(this).data('id'));
//        });

//        $('#btnChooseImages').off('click').on('click', function (e) {
//            e.preventDefault();
//            var finder = new CKFinder();
//            finder.selectActionFunction = function (url) {
//                $('#imageList').append('<div style="float:left"><img src="' + url + '"width="150" /><a href="#" style="color:red" id="btn-deleteImage"><i class="fa fa-times"></i> </a></div>');
//                $('#btn-deleteImage').off('click').on('click', function (e) {
//                    e.preventDefault();
//                    $(this).parent().remove();
//                });
//            };
//            finder.popup();
//        });

//        $('#btnSaveImages').off('click').on('click', function () {
//            var images = [];
//            $.each($('#imageList img'), function (i, item) {
//                images.push($(item).pro('src'));
//            })
//            $.ajax({
//                url: '/Admin/Quanlysanpham/SaveImages',
//                type: 'POST',
//                data: {
//                    images: JSON.stringify(images)
//                },
//                datetype: 'json',
//                success: function (reponse) {
//                    $('#imagesManange').modal('hide');
//                }
//            })
//        })


//    }
//}
//product.init();


(function (app) {

})