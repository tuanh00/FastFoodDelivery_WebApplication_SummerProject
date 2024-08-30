// Import the functions you need from the SDKs you need
import { initializeApp } from "firebase/app";
import { getStorage } from "firebase/storage";

// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
// For Firebase JS SDK v7.20.0 and later, measurementId is optional
const firebaseConfig = {
  apiKey: "AIzaSyCtIYpr4Ozrq8nD1Jsr3klJTi5_6bDql8Y",
  authDomain: "fast-food-delivery-bc243.firebaseapp.com",
  projectId: "fast-food-delivery-bc243",
  storageBucket: "fast-food-delivery-bc243.appspot.com",
  messagingSenderId: "188742917550",
  appId: "1:188742917550:web:0260d642dfd496958dbc3f",
  measurementId: "G-NZCEY05895",
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);
const storage = getStorage(app);

export { storage };
