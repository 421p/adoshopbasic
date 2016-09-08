$(document).ready(function() {

    let context = {
        suppliers : [],
        goods : [],
        categories : []
    };

    let defers = [];

    for (let i in context) {
        let defer = $.Deferred();
        defers.push(defer);

        $.ajax('/'+i).then(function (data) {
            context[i] = JSON.parse(data);
            defer.resolve();
        });
    }

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

            console.log(i);

            if (i % 4 === 0) {
                $('.board').append('<div class="row cells4 row-number-' + i + '"></div>');
                board = $('.board > .row.row-number-' + i);
            }

            board.append("<div class = 'good command-button cell'>" +
                "<span class='icon mif-shopping-basket2'></span>" +
                "<span class='name text-accent'>" + good.Name + "</span><br>" +
                "<span class='count text-default'>Quantity: " + good.Count + "</span><br>" +
                "<span class='price text-default'>Price: " + good.Price + "</span><br>" +
                "<span class='category text-default'>Category: " + good.Category + "</span><br>" +
                "<span class='supplier text-default'>Supplier: " + good.Supplier + "</span></div>");
        })
    });
});