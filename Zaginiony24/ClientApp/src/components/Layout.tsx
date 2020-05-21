import React, { Fragment, Component } from "react";
import { Container } from "reactstrap";
import NavMenu from "./NavMenu";
import ModalContainer from "./modals/ModalContainer";

export class Layout extends Component {
  static displayName = Layout.name;

  render() {
    return (
      <Fragment>
        <ModalContainer />
        <Fragment>
          <NavMenu />
          <Container>{this.props.children}</Container>
        </Fragment>
      </Fragment>
    );
  }
}
