import React, { useState, useContext, useEffect } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { RootStoreContext } from '../stores/rootStore';
import { Link } from 'react-router-dom';
import Login from './User/Login'
import './NavMenu.css';
import Register from './User/Register';

const NavMenu = () => {
  const rootStore = useContext(RootStoreContext);
  const { user, logout, isAdmin } = rootStore.userStore;
  const { openModal } = rootStore.modalStore;
  const [collapsed, setCollapsed] = useState(true);

  useEffect(() => {
    
  }, [user, isAdmin])

  const toggleNavbar = () => {
    setCollapsed(!collapsed);
  }

  return (
    <header>
      <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
        <Container>
          <NavbarBrand tag={Link} to="/">Zaginiony24</NavbarBrand>
          <NavbarToggler onClick={toggleNavbar} className="mr-2" />
          <Collapse className="d-sm-inline-flex flex-sm-row-reverse justify-content-between" isOpen={collapsed} navbar>
            <ul className="navbar-nav flex-grow">
              {user ? (
                <ul className="navbar-nav flex-grow">
                  <NavItem>
                    <NavLink tag={Link} className="text-dark" to="/">{user.username}</NavLink>
                  </NavItem>
                  <NavItem>
                    <NavLink tag={Link} onClick={logout} className="text-dark" to="/">Wyloguj</NavLink>
                  </NavItem>
                </ul>
              ) :
                (
                  <ul className="navbar-nav flex-grow">
                    <NavItem>
                      <NavLink tag={Link} onClick={() => openModal(<Login />, null)} to='#' className="text-dark">Zaloguj</NavLink>
                    </NavItem>
                    <NavItem>
                      <NavLink tag={Link} onClick={() => openModal(<Register />, null)} to='#' className="text-dark">Zarejestruj</NavLink>
                    </NavItem>
                  </ul>
                )}
            </ul>
            <ul className="navbar-nav flex-grow">
              <NavItem>
                <NavLink tag={Link} className="text-dark" to="/">Strona główna</NavLink>
              </NavItem>
              <NavItem>
                {user ? (
                  <NavLink tag={Link} className="text-dark" to="/notice/addnotice">Zgłoś zaginięcie</NavLink>
                ) : (
                    <NavLink tag={Link} onClick={() => openModal(<Login />, null)} className="text-dark" to="#">Zgłoś zaginięcie</NavLink>
                  )}
              </NavItem>
              {user && isAdmin && (
                <NavItem>
                  <NavLink tag={Link} to='/manage' className="">Zarządzaj</NavLink>
                </NavItem>
              )}

            </ul>
          </Collapse>
        </Container>
      </Navbar>
    </header>
  );

}

export default NavMenu;
