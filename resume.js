//**Full Disclaimer**
//I could totally do this in pure javascript and I think that I have figrued out how to do this,
//however upon further examination of my situtation, I am going to use JQuery, because instead of having
//lots of lines of code, I can shorten it to just a couple
//WHEREFORE :) I'm just going to animate with JQuery :)



//https://stackoverflow.com/questions/7002039/easiest-way-to-toggle-2-classes-in-jquery


function toggleSlide(selectedCard) {
    var content = selectedCard.getElementsByClassName("resumeInfo");
    if (content[0].style.display === "none") {
        $(content).slideDown();
    } else {
        $(content).slideUp();
    }
    var picture = selectedCard.getElementsByClassName("arrowIcon")
    $(picture).toggleClass("up down");

}

function toggleAllSlides() {

}

$(document).ready(function() {
    toggleAllSlides();
});


//SlidePanels
//get an instance of the slideHolder
var resumeCardHolder = document.getElementById("resumeCardHolder");
//Get all of the cards
var allCards = resumeCardHolder.getElementsByClassName("resumeCard");
//add Click event to each card
for (var i = 0; i < allCards.length; i++) {
    allCards[i].addEventListener("click", function(event) {
        event.preventDefault();
        toggleSlide(this);
    }, false);
    allCards[i].click();
}