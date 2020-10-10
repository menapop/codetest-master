import '@webcomponents/webcomponentsjs/webcomponents-bundle';
import '@webcomponents/webcomponentsjs/custom-elements-es5-adapter.js';
import '@webcomponents/webcomponentsjs/webcomponents-loader.js';

import DomUtil from "./Utilities/DomUtil";
import { UserModel } from "./WebComponents/UserModal.component";
import { DeleteUserModel } from './WebComponents/DeleteUserModal.component';
import { NewUserModel } from './WebComponents/NewUserModal.component';

export default class App {
  constructor() { }

  execute() {
    try {
        this.setListeners();
        this.setDeleteListeners();
        this.setNewListeners();
    } catch (error) { }
  }

  setListeners() {
      var usersRows = document.querySelectorAll(".user");
    [].slice
      .call(usersRows)
      .forEach((element: Element, index: number, array: HTMLElement[]) => {
        element.addEventListener("click", () => {
          const domUtil = new DomUtil(element);
          const userId = domUtil.getDataAttr("userid");

          const userElement = new UserModel();
          userElement.setAttribute("user-id", userId);
          document.documentElement.append(userElement);
        }, false);
      });
    }


    setDeleteListeners() {
        var usersRows = document.querySelectorAll(".delete");
        [].slice
            .call(usersRows)
            .forEach((element: Element, index: number, array: HTMLElement[]) => {
                element.addEventListener("click", () => {
                    const domUtil = new DomUtil(element);
                    const userId = domUtil.getDataAttr("userid");

                    const userElement = new DeleteUserModel();
                    userElement.setAttribute("user-id", userId);
                    document.documentElement.append(userElement);
                }, false);
            });
    }


    setNewListeners() {
        var usersRows = document.querySelectorAll(".new");
        [].slice
            .call(usersRows)
            .forEach((element: Element, index: number, array: HTMLElement[]) => {
                element.addEventListener("click", () => {
                    const domUtil = new DomUtil(element);
                    const userId = domUtil.getDataAttr("userid");

                    const userElement = new NewUserModel();
                    userElement.setAttribute("user-id", userId);
                    document.documentElement.append(userElement);
                }, false);
            });
    }
}

document.onreadystatechange = () => {
  if (document.readyState == "interactive") {
    new App().execute();
  }
};