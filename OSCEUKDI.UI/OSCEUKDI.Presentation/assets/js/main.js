const main = document.querySelector(".main");
const loginBtn = document.querySelector(".login-btn");
const signupBtn = document.querySelector(".signup-btn");
const loginCancelBtn = document.querySelector(".login-cancel-btn");
const signUpCancelBtn = document.querySelector(".sign-up-cancel-btn");
const sideSignUp = document.querySelector(".side-sign-up");
const sideLogin = document.querySelector(".side-login");

loginBtn.addEventListener("click", toggleLogin);
loginBtn.addEventListener("click", toggleMain);
signupBtn.addEventListener("click", toggleSignup);
signupBtn.addEventListener("click", toggleMain);
signUpCancelBtn.addEventListener("click", untoggleSignup);
loginCancelBtn.addEventListener("click", untoggleLogin);

function toggleMain() {
  main.classList.add("main-active");
}

function toggleLogin() {
  sideLogin.classList.add("side-login-active");
}
function toggleSignup() {
  sideSignUp.classList.add("side-sign-up-active");
}

function untoggleLogin() {
  sideLogin.classList.remove("side-login-active");
  main.classList.remove("main-active");
}
function untoggleSignup() {
  sideSignUp.classList.remove("side-sign-up-active");
  main.classList.remove("main-active");
}
