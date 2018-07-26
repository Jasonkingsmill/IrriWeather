import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { App } from './App';
import { Zones } from './components/irrigation/zones/Zones';

export const routes = <Layout>
    <Route exact path='/' component={Home} />
    <Route exact path='/weather' component={App} />
    <Route exact path='/irrigation' component={Zones} />
    <Route exact path='/irrigation/zones' component={Zones} />
    <Route exact path='/settings' component={App} />

</Layout>;

//    <Route path='/irrigation' component={Zones} />