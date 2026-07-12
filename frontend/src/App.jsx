import { BrowserRouter,Routes,Route } from "react-router-dom"
import Navbar from "./components/Navbar";

import HomePage from "./Pages/HomePage";
import LoginPage from "./Pages/LoginPage"
import RegisterPage from "./Pages/RegisterPage"
import ProductsPage from "./Pages/ProductsPage"
import OrdersPage from "./Pages/OrdersPage"
import ProfilePage from "./Pages/ProfilePage"
import AdminPage from "./Pages/AdminPage"
import ProductDetailPage from "./Pages/ProductDetailPage";
import CartPage from "./Pages/CartPage";
import NotificationCenter from "./components/NotificationCenter";
import AccountStatusWatcher from "./components/AccountStatusWatcher";
import { LanguageProvider } from "./i18n/LanguageContext";

function App() {
  return (
   <BrowserRouter>
   <LanguageProvider>
      <Navbar />
      <AccountStatusWatcher />
      <NotificationCenter />
      <Routes>
         <Route path="/" element={<HomePage />} />
         <Route path="/product/:id" element={<ProductDetailPage />} />
         <Route path="/login" element={<LoginPage />} />
         <Route path="/register" element={<RegisterPage />} />
         <Route path="/products" element={<ProductsPage />} />
         <Route path="/orders" element={<OrdersPage />} />
         <Route path="/cart" element={<CartPage />} />
         <Route path="profile" element={<ProfilePage />} />
         <Route path="/admin" element={<AdminPage />} />
      </Routes>
   </LanguageProvider>

   </BrowserRouter>
  )
}

export default App
