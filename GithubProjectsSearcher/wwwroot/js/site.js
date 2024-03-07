let searchBtn = document.getElementById("searchBtn");
let deleteBtn = document.getElementById("deleteBtn");
let requestInput = document.getElementById("repoInput");
let cardsContainer = document.getElementById("cardsContainer");

let requestId;

searchBtn.addEventListener("click", SearchRepos);
deleteBtn.addEventListener("click", DeleteRepos);


async function DisplayRepos()
{
    const repoResponse = await fetch("api/find",
    {
        method: "GET",
        headers:
        {
            "Accept": 'text/plain;text/html;application/json',
            'Content-Type': 'text/html'
        }
        });

    // если запрос прошел нормально
    if (repoResponse.ok === true)
    {
        let html = await repoResponse.text();
        cardsContainer.innerHTML = html;
    }
    else {
        const error = await response.json();
        console.log(error.message);
    }

};

async function SearchRepos()
{

    if (requestInput.value === "")
    {
        alert("Enter repo name");
        return;
    }

    const response = await fetch("api/find", {
        method: "Post",
        headers:
        {
            "Accept": "application/json", 
            "Content-Type": "application/json"
        },
        body: JSON.stringify({ requestText: requestInput.value })
        
    });

    if (response.ok === true)
    {
        DisplayRepos();
        requestId = getCookie("requestId");
    }
    else {
        const error = await response.json();
        console.log(error.message);
    }
}

async function DeleteRepos()
{
    if (requestId === null)
    {
        return;
    }

    const response = await fetch(`/api/find/${requestId}`,
    {
        method: "DELETE",
        headers:
        {
            "Accept": "application/json;text/plain" 
        }
    });
    if (response.ok === true)
    {
        cardsContainer.innerHTML = null;
        requestInput.value = null;
        document.cookie = 'requestId=; path=/; expires=-1';
    }
    else {
        const error = await response.json();
        console.log(error.message);
    }


}
function getCookie(name) {
    var matches = document.cookie.match(new RegExp(
        "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
    ));
    return matches ? decodeURIComponent(matches[1]) : undefined;
}




