import React, { Component, useState } from "react";
import axios from "axios";

export function HomeComponent(props) {
  let imageUrl =
    "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a7/React-icon.svg/1200px-React-icon.svg.png";

  let [name, setNameState] = useState("maria");
  let [html, setHtmlState] = useState("");

  function setName(el) {
    setNameState(el.target.value);
  }
  function setHtml(el) {
    setHtmlState(el.target.value);
  }

  return (
    <div>
      <textarea
        data-id="content1"
        data-editable=""
        value={html}
        onChange={setHtml}
      ></textarea>
      <input type="text" value={name} onChange={setName} />
      <br />
      <h3>{name}</h3>
      <img src={imageUrl} height={200} width={400} />
      <div dangerouslySetInnerHTML={{ __html: html }} />
    </div>
  );
}
