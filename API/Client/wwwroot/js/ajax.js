﻿$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon"
}).done((result) => {
    console.log(result.results);
    text = "";
    no = 1;
    $.each(result.results, function (key, val) {
        text += `<tr>
                <td>${no++}</td>
                <td>${val.name}</td>
                <td>${val.url}</td>
                <td>
                    <button type="button" value= "${val.url}" onclick='fillData(this.value)' class="btn btn-primary" data-toggle="modal" data-target="#exampleModal">
                        Detail
                    </button>
                </td>`;
    });
    $("#ajaxSW").html(text);
}).fail((error) => {
    console.log(error);
});

let fillData = (val) => {
    $.ajax({
        url: val
    }).done((result) => {
        cekAbility = "";
        $.each(result.abilities, function (key, val) {
            cekAbility += (val.ability.name + ",&nbsp");
        });
        text = `<div class="container"><div class="text-center"><img src="${result.sprites.front_shiny}" alt="" /></div>
            <div class="row">
                <div class="col">Nama</div>
                <div class="col">${result.name}</div>
            </div>
            <div class="row">
                <div class="col">Ability</div>
                <div class="col">${cekAbility}</div>
            </div>
            <div class="row">
                <div class="col">Height</div>
                <div class="col">${result.height}</div>
            </div>
            <div class="row">
                <div class="col">Weight</div>
                <div class="col">${result.weight}</div>
            </div> </div>
        `;
        $("#detailModal").html(text);
    }).fail((error) => {
        console.log(error);
    });
}