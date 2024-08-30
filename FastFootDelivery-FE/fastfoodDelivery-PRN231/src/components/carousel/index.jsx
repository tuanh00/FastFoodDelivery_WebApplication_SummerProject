import React, { useEffect, useRef, useState } from "react";
// Import Swiper React components
import { Swiper, SwiperSlide } from "swiper/react";
import { ShoppingCartOutlined } from "@ant-design/icons";
// import "react-toastify/dist/ReactToastify.css";
// Import Swiper styles
import "swiper/css";
import "swiper/css/pagination";

import "./index.scss";

// import required modules
import { Autoplay, Pagination } from "swiper/modules";
import axios from "axios";
import { Button, Card } from "antd";
import Meta from "antd/es/card/Meta";
import { useDispatch, useSelector } from "react-redux";
import { addFood } from "../../redux/features/fastfoodCart";
import { toast } from "react-toastify";
import { useNavigate } from "react-router-dom";

//

export default function Carousel({
  numberOfSlides = 1,
  Category = "Trending",
  autoplay = false,
}) {
  console.log(numberOfSlides);
  const dispatch = useDispatch();
  const [fastfoods, setFastFood] = useState([]);
  const navigate = useNavigate();
  const user = useSelector((store) => store.accountmanage);

  const handleGetByFastFoodId = async (foodId) => {
    console.log("Get Fast Food by Id", foodId);
  };

  const fetchFastFood = async () => {
    const response = await axios.get(
      "https://localhost:7173/api/MenuItemFood/ViewAllFoods"
    );

    setFastFood(response.data.data);
  };

  useEffect(() => {
    fetchFastFood();
  }, []);

  function handlegetValue(e) {
    console.log(e);
    if (user === null) {
      navigate("/login");
      toast.error("Please Login");
    } else {
      dispatch(
        addFood({
          id: e.foodId,
          name: e.foodName,
          description: e.description,
          price: e.unitPrice,
          quantity: 1,
          image: e.image,
        })
      );
      console.log("hi");
      return toast.success("Add Food Successfully");
    }
  }

  return (
    <>
      {/* <ToastContainer /> */}
      <h3>{Category}</h3>
      <Swiper
        spaceBetween={20}
        slidesPerView={numberOfSlides}
        pagination={true}
        modules={autoplay ? [Pagination, Autoplay] : [Pagination]}
        className={`carousel ${numberOfSlides > 1 ? "multi-item" : ""}`}
        autoplay={{
          delay: 2500,
          disableOnInteraction: false,
        }}
      >
        {fastfoods
          .filter((fastfood) => fastfood.categoryName === Category)
          .map((fastfood) => (
            <SwiperSlide>
              {/* <img src={fastfood.image} alt="" /> */}
              <Card
                className="Card"
                hoverable
                style={{ width: 240 }}
                cover={<img src={fastfood.image} alt="" />}
                onClick={() => {
                  handlegetValue(fastfood);
                }}
              >
                <Meta
                  title={fastfood.foodName}
                  description={fastfood.unitPrice}
                />
                <ShoppingCartOutlined className="carousel__cart" />
              </Card>
            </SwiperSlide>
          ))}
      </Swiper>
    </>
  );
}
