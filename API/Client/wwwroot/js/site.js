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
for (let i = 0; i < animals.length; i++) {
    if (animals[i].species == 'cat') {
        console.log(animals[i]);
    }
}

for (let i = 0; i < animals.length; i++) {
    if (animals[i].class.name == 'mamalia') {
        animals[i].class.name = "Mamalia";
        console.log(animals[i]);
    } else {
        animals[i].class.name = "Non-Mamalia";
        console.log(animals[i]);
    }
}
