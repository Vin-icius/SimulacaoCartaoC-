using Microsoft.AspNetCore.Mvc;

namespace Projeto1Bim.Controllers
{
    public class CartoesController : Controller
    {
        private readonly Projeto1Bim.Service.CartaoService _cartaoService;

        public CartoesController(Service.CartaoService cartaoService)
        {
            _cartaoService = cartaoService;
        }

        /// <summary>
        ///•    Este endpoint recebe o número do cartão de crédito e retorna sua bandeira (VISA, MASTERCARD, ELO...) de acordo com a regra de negócio fictícia dada a seguir, que considera os primeiros 4 dígitos do número e o 8º do cartão (BIN): 
        /// o	1111-XXX-X1X-XXX: VISA
        /// o	2222-XXX-X2X-XXX: MASTERCARD
        /// o	3333-XXX-X3X-XXX: ELO
        /// </summary>
        /// <returns>
        /// •	Retorne 200 + a bandeira do cartão ou 404.
        /// </returns>
        [Obsolete]
        [HttpGet("{cartao}/obter-bandeira")]
        public ActionResult ObterBandeira(string cartao)
        {
            var _cartao = _cartaoService.ObterBandeira(cartao);

            if (_cartao != null)
                return Ok(_cartao);
            else return NotFound();
        }

       

        // GET: CartoesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CartoesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CartoesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CartoesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CartoesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CartoesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
