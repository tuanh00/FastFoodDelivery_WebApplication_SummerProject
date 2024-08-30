import React from "react";
import "./index.scss";

function Footer() {
  return (
    <div className="footer">
      <div className=" container footer__common">
        <div className="accordian">
          <div className="footer_card">
            <div className="card_header">
              <h6>Danh Mục Món Ăn</h6>
            </div>
            <div className="card_body">
              <ul>
                <li>Ưu Đãi</li>
                <li>Món Mới</li>
                <li>Món Ăn Nhẹ</li>
                <li>Thức Uống & Tráng Miệng</li>
              </ul>
            </div>
          </div>
          <div className="footer_card">
            <div className="card_header">
              <h6>Liên Hệ Fast Food Delivery</h6>
            </div>
            <div className="card_body">
              <ul>
                <li>Theo dõi đơn hàng</li>
                <li>Hệ Thống Nhà hàng</li>
                <li>Liên hệ Fast Food Delivery</li>
              </ul>
            </div>
          </div>
          <div className="footer_card">
            <div className="card_header">
              <h6>Thông Tin liên hệ</h6>
            </div>
            <div className="card_body">
              <ul>
                <li>Phone: 012398664</li>
                <li>Địa Chỉ: FPT University</li>
              </ul>
            </div>
          </div>
        </div>
        <div className="social_link">
          <p>FPT University © 2024 Fast Foot Delivery</p>
          <div className="app_footer_bottom">
            <img src="/bank-image/instagramicon.png" width={50} height={50} />
            <img src="/bank-image/facebooknew.png" width={50} height={50} />
            <img src="/bank-image/tiktoknew.jpg" width={50} height={50} />
          </div>
        </div>
      </div>
    </div>
  );
}

export default Footer;
