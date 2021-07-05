// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const animals = [
    { name: "Fluffy", species: "cat", class: { name: "mamalia" } },
    { name: "Carlo", species: "dog", class: { name: "vertebrata" } },
    { name: "Nemo", species: "fish", class: { name: "mamalia" } },
    { name: "Hamilton", species: "dog", class: { name: "mamalia" } },
    { name: "Dory", species: "fish", class: { name: "mamalia" } },
    { name: "Ursa", species: "cat", class: { name: "mamalia" } },
    { name: "Taro", species: "cat", class: { name: "vertebrata" } }
];

//Menampilkan animal yang species cat
for (let i = 0; i < animals.length; i++) {
    if (animals[i].species == 'cat') {
        console.log(animals[i]);
    }
}

//manipulasi string utk object kelas, jika mamalia (tulis Mamalia), jika bukan mamalia (tulis Non-Mamalia)
for (let k = 0; k < animals.length; k++) {
    if (animals[k].class.name == 'mamalia') {
        animals[k].class.name = "Mamalia";
        console.log(animals[k]);
    } else {
        animals[k].class.name = "Non-Mamalia";
        console.log(animals[k]);
    }
}


$.ajax({
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
                    <button type="button" value= "${val.url}"
                        onclick='fillData(this.value)' class="btn btn-primary" data-toggle="modal" data-target="#exampleModal">
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
            cekAbility += `<span class="badge badge-primary">${val.ability.name}</span> &nbsp`;
        });
        text = `<div class="container"><div class="text-center"><img src="${result.sprites.front_shiny}" alt="" /></div>
            <div class="row">
                <div class="col">Name</div>
                <div class="col">${result.name}</div>
            </div>
            <div class="row">
                <div class="col">Abilities</div>
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

