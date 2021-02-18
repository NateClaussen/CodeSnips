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
    //This is a comment to see if my snippet actually changes and updates! :O
}

$(document).ready(function() {
    toggleAllSlides();
});

//SlidePanels
var resumeCardHolder = document.getElementById("resumeCardHolder");
var allCards = resumeCardHolder.getElementsByClassName("resumeCard");
for (var i = 0; i < allCards.length; i++) {
    allCards[i].addEventListener("click", function(event) {
        event.preventDefault();
        toggleSlide(this);
    }, false);
    allCards[i].click();
}