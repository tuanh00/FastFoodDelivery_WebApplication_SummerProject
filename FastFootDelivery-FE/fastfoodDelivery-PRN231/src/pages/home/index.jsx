import Meta from "antd/es/card/Meta";
import React, { useEffect, useState } from "react";
import Carousel from "../../components/carousel";
import Header from "../../components/header";
import "./index.scss";
import { Swiper, SwiperSlide } from "swiper/react";
// import "react-toastify/dist/ReactToastify.css";
// Import Swiper styles
import "swiper/css";

// import required modules
import { Autoplay } from "swiper/modules";
import Item from "antd/es/list/Item";
import axios from "axios";
import { Card } from "antd";
import { ShoppingCartOutlined } from "@ant-design/icons";
import { useDispatch, useSelector } from "react-redux";
import { addFood } from "../../redux/features/fastfoodCart";
import { toast } from "react-toastify";
import { Navigate, useNavigate } from "react-router-dom";

function HomePage() {
  const [dataCategory, setDataCategory] = useState([]);
  const [dataSearch, setDataSearch] = useState([]);
  const [check, setCheck] = useState(false);
  const [search, setSearch] = useState(false);
  const user = useSelector((store) => store.accountmanage);
  const navigate = useNavigate();

  const dispatch = useDispatch();

  const fetchCategories = async () => {
    try {
      const response = await axios.get(
        "https://localhost:7173/api/Category/ViewAllCategorys"
      );
      console.log(response.data);
      setDataCategory(response.data.data);
    } catch (e) {
      console.log(e);
    }
  };

  useEffect(() => {
    fetchCategories();
  }, [check]);

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
    <div className="HomePage">
      {/* <ToastContainer /> */}
      <Header
        setDataSearch={setDataSearch}
        setCheck={setCheck}
        setSearch={setSearch}
      />

      {/* <div className="HomePage__image">
        <img src="/pannel.jpg" />
      </div> */}
      <div>
        <Swiper
          autoplay={{
            delay: 2500,
            disableOnInteraction: false,
          }}
          modules={[Autoplay]}
          className="mySwiper"
        >
          <SwiperSlide>
            <img
              src="https://static.kfcvietnam.com.vn/images/content/home/carousel/xs/BO.webp?v=gqNVl3"
              alt=""
            />
          </SwiperSlide>
          <SwiperSlide>
            <img
              src="https://static.kfcvietnam.com.vn/images/content/home/carousel/xs/combochuannhat.webp?v=gqNVl3"
              alt=""
            />
          </SwiperSlide>
          <SwiperSlide>
            <img
              src="https://static.kfcvietnam.com.vn/images/content/home/carousel/xs/HTKU.webp?v=gqNVl3"
              alt=""
            />
          </SwiperSlide>
        </Swiper>
      </div>

      {/* <Carousel autoplay /> */}
      {!search ? (
        dataCategory?.map((item) => (
          <div className="menu-row">
            <h2>
              <img src="/bank-image/icon5.jpg" width={80} height={80}></img>
              <span>Bạn Có thể thích món này</span>
              <div className="line"></div>
            </h2>
            <Carousel
              numberOfSlides={5}
              Category={item.categoriesName}
            ></Carousel>
          </div>
        ))
      ) : dataSearch.length == 0 ? (
        <div>Not Found</div>
      ) : (
        <div>
          <h2>
            <span>Result Search</span>
          </h2>
          <Swiper
            spaceBetween={20}
            slidesPerView={4}
            autoplay={{
              delay: 2500,
              disableOnInteraction: false,
            }}
            pagination={true}
            modules={[Autoplay]}
            className="SwiperSearch"
          >
            {dataSearch.map((item) => (
              <div className="searchResult">
                {/* {item.} */}
                <SwiperSlide className="slideResearch">
                  <Card
                    className="CardSearch"
                    hoverable
                    cover={<img src={item.image} alt="" />}
                    onClick={() => {
                      handlegetValue(item);
                    }}
                  >
                    <Meta title={item.foodName} description={item.unitPrice} />
                    <ShoppingCartOutlined className="search__cart" />
                  </Card>
                </SwiperSlide>
              </div>
            ))}
          </Swiper>
        </div>
      )}
      {/* <div className="menu-row">
        <h2>
          <img src="/bank-image/icon5.jpg" width={80} height={80}></img>
          <span>Bạn Có thể thích món này</span>
          <div className="line"></div>
        </h2>
        <Carousel numberOfSlides={5} Category="FastFood"></Carousel>
      </div>

      <div className="menu-row">
        <h2>
          <img src="/bank-image/icon5.jpg" width={80} height={80}></img>
          <span>Bạn Có thể thích món này</span>
          <div className="line"></div>
        </h2>
        <Carousel numberOfSlides={5} Category="Burger"></Carousel>
      </div> */}
    </div>
  );
}

export default HomePage;
