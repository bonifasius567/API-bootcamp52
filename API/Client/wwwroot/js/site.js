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
