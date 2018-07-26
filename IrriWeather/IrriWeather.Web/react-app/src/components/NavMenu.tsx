import * as React from 'react';
import { NavLink, Link } from 'react-router-dom';


export class NavMenu extends React.Component<{}, {}> {
    public render() {
        return <div className='main-nav'>
            <div className='navbar navbar-inverse'>
                <div className='navbar-header'>
                    <button type='button' className='navbar-toggle' data-toggle='collapse' data-target='.navbar-collapse'>
                        <span className='sr-only'>Toggle navigation</span>
                        <span className='icon-bar'></span>
                        <span className='icon-bar'></span>
                        <span className='icon-bar'></span>
                    </button>
                    <Link className='navbar-brand' to={'/'}>IrriWeather</Link>
                </div>
                <div className='clearfix'></div>
                <div className='navbar-collapse collapse'>
                    <ul className='nav navbar-nav'>
                        <li>
                            <NavLink exact to={'/'} activeClassName='active'>
                                <span className='glyphicon glyphicon-home'></span> Home
                            </NavLink>
                        </li>
                        <li>
                            <NavLink to={'/weather'} activeClassName='active'>
                                <span className='glyphicon glyphicon-cloud'></span> Weather
                            </NavLink>
                        </li>
                        <li>
                            <NavLink to={'/irrigation'} activeClassName='active'>
                                <span className='glyphicon glyphicon-leaf'></span> Irrigation
                            </NavLink>
                        </li>
                        <li>
                            <NavLink to={'/settings'} activeClassName='active'>
                                <span className='glyphicon glyphicon-cog'></span> Settings
                            </NavLink>
                        </li>
                    </ul>
                </div>
            </div>
        </div>;
    }
}
