import React from "react";
import style from '../../styles/notice.module.scss'
import placeholder from '../../pictures/120x150.png'
import { INotice } from '../../models/notice'
import { Link } from "react-router-dom";

const Notice: React.FC<{ notice: INotice }> = ({ notice }) => {
    return (
        <div className={style.itemwrap}>
            <Link className={style.link} to={`/notice/details/${notice.id}`}>
            <div className={style.picture}>
                <img src={placeholder} alt='' />
            </div>
            <div className={style.details}>
                <h5 className={style.title}>{notice.name} {notice.surname}</h5>
                <span className={style.age}>Lat: {notice.age}</span><br />
                <span>Płeć: {notice.gender}</span><br />
                <span>Miejsce zaginięcia: {notice.lastSeenPlace}</span><br />
                <span>Województwo: {notice.district}</span><br />
                <span>Data zaginięcia: {notice.dateOfDisappearance}</span>
            </div>
            </Link>
        </div >

        );
};

export default Notice;