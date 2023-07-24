var product = {
    init: function () {
        product.registerEvents();
    },
    registerEvents: function () {

        $('.btn-img').off('click').on('click', function (e) {

            e.preventDefault();
            $('#imagesManange').modal('Show');
            $('#hidProductID').val($(this).data('id'));
        });

       

       
    }
}
product.init();