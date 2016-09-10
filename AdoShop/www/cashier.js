$(document).ready(function() {

    let context = {
        suppliers : [],
        goods : [],
        categories : []
    };

    let key;

    let defers = [];

    for (let i in context) {
        let defer = $.Deferred();
        defers.push(defer);

        $.ajax('/'+i).then(data => {
            context[i] = JSON.parse(data);
            defer.resolve();
        });
    }

    let headersDefer = $.Deferred();

    defers.push(headersDefer);

    $.ajax(document.location).then((data, status, request) => {
        key = request.getResponseHeader("api-key");
        headersDefer.resolve();
    });

    $.when.apply($, defers).done(() => {
        let result = context.goods.map(good => {
            //noinspection JSUnresolvedVariable
            good.Category = context.categories.filter(x => x.Id === good.CategoryId).pop().Name;
            //noinspection JSUnresolvedVariable
            good.Supplier = context.suppliers.filter(x => x.Id === good.SupplierId).pop().Name;

            return good;
        });

        let board = $('.board > .row.row-number-0');

        result.forEach((good, i) => {

            if (i % 4 === 0) {
                $('.board').append('<div class="row cells4 row-number-' + i + '"></div>');
                board = $('.board > .row.row-number-' + i);
            }

            board.append("<div class = 'good command-button cell' data-count='0' data-id='" + good.Id + "'>" +
                "<span class='icon'>0</span>" +
                "<span class='name text-accent'>" + good.Name + "</span><br>" +
                "<span class='count text-default'>Quantity: " + good.Count + "</span><br>" +
                "<span class='price text-default'>Price: " + good.Price + "</span><br>" +
                "<span class='category text-default'>Category: " + good.Category + "</span><br>" +
                "<span class='supplier text-default'>Supplier: " + good.Supplier + "</span></div>");
        });

        $('.good').on('click', function(){
            let quant = $(this).find('.count');
            let count = parseInt($(this).attr('data-count'), 10);
            let price = parseInt($(this).find('.price').text().split(' ')[1], 10);
            let newText = "Quantity: " + (parseInt(quant.text().split(' ')[1], 10) - 1);
            quant.text(newText);
            $(this).attr('data-count', count + 1);
            $(this).find('.icon').text(count + 1);

            $(this).addClass('in-order');
            $(this).addClass('bg-lightTeal');

            let orderSum = parseInt($('.sum-order').text(), 10);
            $('.sum-order').text(orderSum + price);

        });
    });

    $('.create-order').on('click', function(){
        let order = [];

        if ($('.in-order').length === 0) {
            alert('No goods in order!');
            return;
        }

        $('.in-order').each(function(){
           order.push({
               Id: parseInt($(this).attr('data-id')),
               Count: $(this).attr('data-count')
           });

           $(this).attr('data-count', 0);
            $(this).removeClass('in-order');
            $(this).removeClass('bg-lightTeal');
            $(this).find('.icon').text('0');
        });

        $('.sum-order').text('0');

        $.ajax({
            url : '/make/order',
            method : 'POST',
            data : {"Order" : JSON.stringify(order)},
            beforeSend: xhr => xhr.setRequestHeader('Api-Key', key)
        }).then(data => {
            alert(data);
        })
    });

    $('.cancel-order').on('click', function(){
        $('.in-order').each(function(){

            let count = parseInt($(this).attr('data-count'), 10);
            let quant = $(this).find('.count');
            quant.text("Quantity: " + (parseInt(quant.text().split(' ')[1], 10) + count));

            $(this).attr('data-count', 0);
            $(this).removeClass('in-order');
            $(this).removeClass('bg-lightTeal');
            $(this).find('.icon').text('0');

            $('.sum-order').text('0');
        });
    });

});