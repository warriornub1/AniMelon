const loginForm = document.getElementById("loginForm");
const errorElement = document.getElementById("error");

loginForm.addEventListener("submit", async (event) => {
  event.preventDefault();

  const username = document.getElementById("username").value;
  const password = document.getElementById("password").value;

  try {
    const response = await fetch("https://yourapi.com/api/login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ username, password }),
    });

    if (!response.ok) {
      throw new Error("Invalid login credentials");
    }

    const data = await response.json();
    localStorage.setItem("token", data.token); // Save JWT token
    alert("Login successful!");
    window.location.href = "/dashboard.html"; // Redirect to dashboard
  } catch (error) {
    errorElement.textContent = error.message;
  }
});
