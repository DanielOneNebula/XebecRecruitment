var nav;

function initalizeSlideOutNav()
{
    nav = document.getElementById("testNav")   
}


function openNav() {
    nav.style.width = "300px"; 
}
  
function closeNav() {
    nav.style.width = "0"; 
}