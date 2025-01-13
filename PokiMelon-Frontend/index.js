const apiUrl = "http://localhost:5087";
const passageContainer = document.querySelector(".PassageContainer");

const getAllPassage = async () => {
  try {
    const res = await fetch(apiUrl + "/Passage/GetAllPassage");
    const data = await res.json();

    data.forEach((d) => {
      const passageDiv = document.createElement("div");
      passageDiv.classList.add("passage");
      passageDiv.innerHTML = `
        <h3>${d.title}<h3>
        <p>${d.passage}</p>
      `;
      passageDiv.addEventListener("click", () => {
        document.location.href = `TextToSpeech.html?id=${d.id}`;
      });
      passageContainer.appendChild(passageDiv);
    });
  } catch (error) {
    console.log(error);
  }
};

getAllPassage();
