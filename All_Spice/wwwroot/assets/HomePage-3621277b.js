import{_ as n,d as p,r as i,P as d,b as l,R as m,e as _,o as e,c as r,F as u,f,A as g,g as R}from"./index-81799fdb.js";const P={setup(){p(()=>{s()});async function s(){try{await i.getRecipes()}catch(c){d.error(c.message,"[GETTING RECIPES]")}}return{recipes:l(()=>g.recipes)}},components:{RecipeCard:m}},y={class:"row my-3"};function C(s,c,x,t,E,h){const a=_("RecipeCard");return e(),r("section",y,[(e(!0),r(u,null,f(t.recipes,o=>(e(),r("div",{key:o.id,class:"col-12 col-md-6 col-lg-4 px-5 py-3"},[R(a,{recipeProp:o},null,8,["recipeProp"])]))),128))])}const v=n(P,[["render",C]]);export{v as default};